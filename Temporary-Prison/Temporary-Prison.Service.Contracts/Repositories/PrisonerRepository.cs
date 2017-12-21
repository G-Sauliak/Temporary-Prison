using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Extensions;

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
                        var dataTableModel = new DataTable();
                        var dataTableArray = new DataTable();
                        try
                        {
                            dataTableModel.Load(dataReader);
                            dataTableArray.Load(dataReader);

                            prisoner = dataTableModel.ConvertToModel<PrisonerDto>();
                            prisoner.PhoneNumbers = dataTableArray.ConvertToArrayOfStrings("PhoneNumber");
                        }
                        finally
                        {
                            dataTableModel.Dispose();
                            dataTableArray.Dispose();
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

        public bool AddPrisoner(PrisonerDto prisoner, out int newId)
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

        public PrisonerDto[] FindPrisonersByName(string search)
        {
            PrisonerDto[] prisoners = null;
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("FindPrisonersByName", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("search", search);
               
                    using (var dataTable = new DataTable())
                    {
                        dataTable.Load(sqlCommand.ExecuteReader());
                        prisoners = dataTable.ConvertToArrayOfModels<PrisonerDto>();
                    }
                }
            }
            return prisoners;
        }

        public void EditPrisoner(PrisonerDto prisoner)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("UpdatePrisoner", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    var param = new SqlParameter[]
                     {
                        new SqlParameter(@"Prisoner_ID",prisoner.PrisonerId),
                        new SqlParameter(@"FirstName",prisoner.FirstName),
                        new SqlParameter(@"SurName",prisoner.Surname),
                        new SqlParameter(@"LastName",prisoner.LastName),
                        new SqlParameter(@"PlaceOfWork",prisoner.PlaceOfWork),
                        new SqlParameter(@"BirthDate",prisoner.BirthDate),
                        new SqlParameter(@"Photo",prisoner.Photo),
                        new SqlParameter(@"Address",prisoner.Address),
                        new SqlParameter(@"AdditionalInformation",prisoner.AdditionalInformation),
                        new SqlParameter(@"RelationshipStatus",prisoner.AdditionalInformation),
                     };
                    sqlCommand.Parameters.AddRange(param);

                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = "AddPhoneNumber";

                    foreach (var number in prisoner.PhoneNumbers)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("PhoneNumber", number);
                        sqlCommand.Parameters.AddWithValue("PrisonerId", prisoner.PrisonerId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeletePrisoner(int id)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("DeletePrisoner", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(@"Priosner_ID", id);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
