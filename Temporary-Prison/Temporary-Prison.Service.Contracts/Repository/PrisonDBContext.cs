using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repository
{
    class PrisonDBContext
    {
        private string connectionString;

        public PrisonDBContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PrisonDataBase"].ConnectionString;
        }

        public List<PrisonerDto> GetPrisoners()
        {
            var listPrisoners = new List<PrisonerDto>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("GetPrisoners", sqlConnection))
                {
                    try
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {

                                while (dataReader.Read())
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
                                            placeOfResidence = new PlaceOfResidence()
                                            {
                                                County = dataReader["County"].ToString(),
                                                City = dataReader["City"].ToString(),
                                                Street = dataReader["Street"].ToString(),
                                                HouseNumber = (int)dataReader["HouseNumber"],
                                                ApartmentNumber = (int)dataReader["ApartmentNumber"],

                                            },
                                            PhoneNumbers = new List<string>()
                                        };
                                        listPrisoners.Add(prisoner);
                                    }
                                    listPrisoners.Find(p => p.PrisonerId == id).PhoneNumbers.Add(dataReader["PhoneNumber"].ToString());
                                }
                            }
                            else
                            {
                                //TODO 
                            }

                        }
                    }
                    catch (DbException e)
                    {
                        //TODO
                    }
                }
            }
            return listPrisoners;
        }

    }
}
