using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repository
{
    class PrisonRepository
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

        public List<PrisonerDto> GetPrisoners()
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

                            var id = (int)dataReader["PrisonerId"];

                            var prisoner = listPrisoners.Where(p => p.PrisonerId == id).FirstOrDefault();

                            if (prisoner == null)
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
                                    PhoneNumbers = new string[dataReader.FieldCount]
                                };
                                listPrisoners.Add(prisoner);
                            }
                            listPrisoners.Find(p => p.PrisonerId == id).PhoneNumbers[i] = dataReader["PhoneNumber"].ToString();
                        }
                    }
                }
            }
            return listPrisoners;
        }
    }
}
