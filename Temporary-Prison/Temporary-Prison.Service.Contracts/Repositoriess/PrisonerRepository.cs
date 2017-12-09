using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    class PrisonerRepository : IPrisonerRepository
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
            //GetPrisonerById
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
                                BirthDate = (DateTime)dataReader["BirthDate"],
                                RelationshipStatus = dataReader["RelationshipStatus"].ToString(),
                                Avatar = dataReader["Photo"].ToString(),
                                address = new Address()
                                {
                                    County = dataReader["County"].ToString(),
                                    City = dataReader["City"].ToString(),
                                    Street = dataReader["Street"].ToString(),
                                    HouseNumber = (int)dataReader["HouseNumber"],
                                    ApartmentNumber = (int)dataReader["ApartmentNumber"],
                                },
                                PhoneNumbers = new List<string>()
                            };

                            for (int i = 0; dataReader.NextResult(); i++)
                            {
                                ((List<string>)prisoner.PhoneNumbers).Add(dataReader["PhoneNumber"].ToString());
                            }
                        }
                    }
                }
            }
            return prisoner;
        }

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {

            var listPrisoners = new List<PrisonerDto>();

            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("GetPrisoners", sqlConnection))
                {
                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        for (int i = 0; dataReader.Read(); i++)
                        {
                            var prisoner = new PrisonerDto()
                            {
                                PrisonerId = (int)dataReader["PrisonerId"],
                                FirstName = dataReader["FirstName"].ToString(),
                                LastName = dataReader["LastName"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                PlaceOfWork = dataReader["PlaceOfWork"].ToString(),
                                AdditionalInformation = dataReader["AdditionalInformation"].ToString(),
                                BirthDate = (DateTime)dataReader["BirthDate"],
                                RelationshipStatus = dataReader["RelationshipStatus"].ToString(),
                                Avatar = dataReader["Photo"].ToString(),
                                address = new Address()
                                {
                                    County = dataReader["County"].ToString(),
                                    City = dataReader["City"].ToString(),
                                    Street = dataReader["Street"].ToString(),
                                    HouseNumber = (int)dataReader["HouseNumber"],
                                    ApartmentNumber = (int)dataReader["ApartmentNumber"],

                                },
                            };
                            listPrisoners.Add(prisoner);
                        }
                    }
                }
            }
            return listPrisoners;
        }
    }
}
