using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    public class PrisonerRepository : IPrisonerRepository
    {
        private string GetConnectionString
        {
            get
            {
                var ConStrSettings = ConfigurationManager.ConnectionStrings["PrisonDataBase"];

                if (ConStrSettings == null)
                {
                    throw new NullReferenceException("Connection string is null");
                }

                return ConStrSettings.ConnectionString;
            }
        }

        public PrisonerDto GetPrisonerById(int Id)
        {
            PrisonerDto prisoner = null;

            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("GetPrisonerById", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("ID", Id);

                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            prisoner = new PrisonerDto()
                            {
                                PrisonerId = (int)dataReader["PrisonerId"],
                                FirstName = dataReader["FirstName"].ToString(),
                                LastName = dataReader["LastName"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                PlaceOfWork = dataReader["PlaceOfWork"].ToString(),
                                AdditionalInformation = dataReader["AdditionalInformation"].ToString(),
                                BirthDate = ((DateTime)dataReader["BirthDate"]),
                                RelationshipStatus = dataReader["RelationshipStatus"].ToString(),
                                Photo = dataReader["Photo"].ToString(),
                                Address = dataReader["Address"].ToString(),
                                PhoneNumbers = new List<string>()
                            };
                        }
                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {
                                var s = dataReader.FieldCount;
                                prisoner.PhoneNumbers.Add(dataReader["PhoneNumber"].ToString());
                            }
                        }
                    }
                }
            }
            return prisoner;
        }

        public IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
        {
            var listPrisoners = new List<PrisonerDto>();

            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetPrisonersToPagedList", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                     };
                    var returnVal = sqlCommand.Parameters.Add("@return_value", SqlDbType.Int);

                    returnVal.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.Parameters.AddRange(param);
                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        
                        while (dataReader.Read())
                        {
                            var prisoner = new PrisonerDto()
                            {
                                PrisonerId = (int)dataReader["PrisonerId"],
                                FirstName = dataReader["FirstName"].ToString(),
                                LastName = dataReader["LastName"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                BirthDate = (DateTime)dataReader["BirthDate"],
                            };
                            listPrisoners.Add(prisoner);
                        }

                        dataReader.NextResult();
                       
                        totalCount = (int)returnVal.Value;
                    }
                }
            }
            return listPrisoners;
        }

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
            var listPrisoners = new List<PrisonerDto>();

            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("GetPrisoners", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var prisoner = new PrisonerDto()
                            {
                                PrisonerId = (int)dataReader["PrisonerId"],
                                FirstName = dataReader["FirstName"].ToString(),
                                LastName = dataReader["LastName"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                BirthDate = (DateTime)dataReader["BirthDate"],
                            };
                            listPrisoners.Add(prisoner);
                        }
                    }
                }
            }
            return listPrisoners;
        }

        public bool TryAddPrisoner(PrisonerDto prisoner, out int newId)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("AddPriosner", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter[]
                    {
                        new SqlParameter(@"FirstName",prisoner.FirstName),
                        new SqlParameter(@"LastName",prisoner.LastName),
                        new SqlParameter(@"Surname",prisoner.Surname),
                        new SqlParameter(@"Photo",prisoner.Photo),
                        new SqlParameter(@"PlaceOfWork",prisoner.PlaceOfWork),
                        new SqlParameter(@"Address",prisoner.Address),
                        new SqlParameter(@"BirthDate",prisoner.BirthDate),
                        new SqlParameter(@"AdditionalInformation",prisoner.AdditionalInformation),
                        new SqlParameter(@"RelationshipStatus",prisoner.RelationshipStatus)
                     };

                    var returnVal = sqlCommand.Parameters.Add("@return_value", SqlDbType.Int);
                    returnVal.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.Parameters.AddRange(param);

                    var result = sqlCommand.ExecuteNonQuery();

                    newId = (int)returnVal.Value;

                    sqlCommand.CommandText = "AddPhoneNumber";

                    foreach (var number in prisoner.PhoneNumbers)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("PhoneNumber", number);
                        sqlCommand.Parameters.AddWithValue("PrisonerId", newId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }
    }
}
