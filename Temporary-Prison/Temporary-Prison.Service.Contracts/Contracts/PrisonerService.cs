using log4net;
using System.Data.SqlClient;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonerService : DataAccessService, IPrisonerService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerDto GetPrisonerById(int Id)
        {
            if (Id != default(int))
            {
                var prisoner = GetModel<PrisonerDto>("GetPrisonerById", new SqlParameter(@"prisonerId", Id));
                var phones = ExecProcGetModels<Phone>("GetPhoneNumbers", new SqlParameter(@"prisonerId", Id));

                if (phones != null)
                {
                    prisoner.PhoneNumbers = new string[phones.Length];
                    for (int i = 0; i < phones.Length; i++)
                    {
                        prisoner.PhoneNumbers[i] = phones[i].PhoneNumber;
                    }
                }

                return prisoner;
            }
            return default(PrisonerDto);
        }

        public PrisonerDto[] GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
        {
            if (rowSize != default(int))
            {
                var param = new SqlParameter[]
                     {
                        new SqlParameter(@"skip",skip),
                        new SqlParameter(@"rowSize",rowSize),
                     };
                return ExecProcGetModels<PrisonerDto, int>("GetPrisonersToPagedList", "TotalCount", out totalCount, param);
            }
            totalCount = default(int);
            return default(PrisonerDto[]);
        }

        public bool AddPrisoner(PrisonerDto prisoner, out int newId)
        {
            if (prisoner != null)
            {
                int lasID;
                ExecNonQuery("insertPrisoner", prisoner, "newID", out lasID);
                var countPhones = prisoner.PhoneNumbers.Length;
                var phones = new Phone[countPhones];
                if (lasID != default(int))
                {
                    for (int i = 0; i < countPhones; i++)
                    {
                        phones[i] = new Phone()
                        {
                            PrisonerId = lasID,
                            PhoneNumber = prisoner.PhoneNumbers[i]
                        };
                    }
                    ExecNonQuery("dbo.InsertPhoneNumbers", phones);
                    newId = lasID;
                    return true;
                }
            }
            newId = default(int);
            return default(bool);
        }

        public PrisonerDto[] FindPrisonersByName(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return ExecProcGetModels<PrisonerDto>("FindPrisonersByName", new SqlParameter(@"search", search));
            }
            return default(PrisonerDto[]);
        }

        public void DeletePrisoner(int id)
        {
            if (id != default(int))
            {
                ExecNonQuery("DeletePrisoner", new SqlParameter("@Priosner_ID", id));
            }
        }

        public void EditPrisoner(PrisonerDto prisoner)
        {
            if (prisoner != null)
            {
                ExecNonQuery("EditPrisoner", prisoner);
                var countPhones = prisoner.PhoneNumbers.Length;
                var phones = new Phone[countPhones];
                for (int i = 0; i < countPhones; i++)
                {
                    phones[i] = new Phone()
                    {
                        PrisonerId = prisoner.PrisonerId,
                        PhoneNumber = prisoner.PhoneNumbers[i]
                    };
                }
                ExecNonQuery("dbo.InsertPhoneNumbers", phones);
            }
        }
        public DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount)
        {
            var parametrs = new SqlParameter[]
            {
                new SqlParameter("@PrisonerID",Id),
                new SqlParameter("@skip",skip),
                new SqlParameter("@rowSize",rowSize)
            };

            return ExecProcGetModels<DetentionPagedListDto,int>("GetDetentionsByIdTForPagedList", "totalCount", out totalCount, parametrs);
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {

            if (registrationOfDetention != null)
            {
                ExecNonQuery("insertEmployee", registrationOfDetention.DetainedEmployee,
                    "employeeID", out int _DetainedEmployeeID);
                ExecNonQuery("insertEmployee", registrationOfDetention.DeliveredEmployee,
                    "employeeID", out int _DeliveredEmployeeID);

                ExecNonQuery("InsertDeliveryProcedures",
                    "DeliveryProceduresID", out int _DeliveryProceduresID,
                    new SqlParameter("@employeeID", _DeliveredEmployeeID));

                ExecNonQuery("InsertDetentionProcedures",
                    "DetentionProceduresID", out int _DetentionProceduresID,
                    new SqlParameter("@employeeID", _DetainedEmployeeID));

                var regist = new RegistrationOfDetention()
                {
                    PrisonerID = registrationOfDetention.PrisonerID,
                    DateOfDetention = registrationOfDetention.DateOfDetention,
                    DateOfArrival = registrationOfDetention.DateOfArrival,
                    DeliveredProceduresID = _DeliveryProceduresID,
                    DetentionProceduresID = _DetentionProceduresID,
                    PlaceofDetention = registrationOfDetention.PlaceofDetention
                };
                ExecNonQuery("RegistrationOfDetention", regist);
            }

        }
    }
}
