using log4net;
using System.Data.SqlClient;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonerService : IPrisonerService
    {
        private readonly DataAccessService context = new DataAccessService();
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerDto GetPrisonerById(int Id)
        {
            if (Id != default(int))
            {
                var prisoner = context.ExecProcGetModel<PrisonerDto>("GetPrisonerById", new SqlParameter(@"prisonerId", Id));
                var phones = context.ExecProcGetModels<Phone>("GetPhoneNumbers", new SqlParameter(@"prisonerId", Id));

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
                return context.ExecProcGetModels<PrisonerDto, int>("GetPrisonersToPagedList", "TotalCount", out totalCount, param);
            }
            totalCount = default(int);
            return default(PrisonerDto[]);
        }

        public bool AddPrisoner(PrisonerDto prisoner, out int newId)
        {
            if (prisoner != null)
            {
                context.ExecNonQuery("insertPrisoner", prisoner, "newID", out int lasID);
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
                    context.ExecNonQuery("dbo.InsertPhoneNumbers", phones);
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
                return context.ExecProcGetModels<PrisonerDto>("FindPrisonersByName", new SqlParameter(@"search", search));
            }
            return default(PrisonerDto[]);
        }

        public void DeletePrisoner(int id)
        {
            if (id != default(int))
            {
                context.ExecNonQuery("DeletePrisoner", new SqlParameter("@Priosner_ID", id));
            }
        }

        public void EditPrisoner(PrisonerDto prisoner)
        {
            if (prisoner != null)
            {
                context.ExecNonQuery("EditPrisoner", prisoner);
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
                context.ExecNonQuery("dbo.InsertPhoneNumbers", phones);
            }
        }
        public DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount)
        {
            if (rowSize > 0)
            {
                var parametrs = new SqlParameter[]
                {
                new SqlParameter("@PrisonerID",Id),
                new SqlParameter("@skip",skip),
                new SqlParameter("@rowSize",rowSize)
                };
                return context.ExecProcGetModels<DetentionPagedListDto, int>("GetDetentionsByIdForPagedList", "totalCount", out totalCount, parametrs);
            }
            totalCount = default(int);
            return default(DetentionPagedListDto[]);
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {
            if (registrationOfDetention != null)
            {
                context.ExecNonQuery("insertEmployee",
                    registrationOfDetention.DetainedEmployee,
                    "employeeID", out int _DetainedEmployeeID);

                context.ExecNonQuery("insertEmployee",
                    registrationOfDetention.DeliveredEmployee,
                    "employeeID", out int _DeliveredEmployeeID);

                context.ExecNonQuery("InsertDeliveryProcedures",
                    "DeliveryProceduresID", out int _DeliveryProceduresID,
                    new SqlParameter("@employeeID", _DeliveredEmployeeID));

                context.ExecNonQuery("InsertDetentionProcedures",
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
                context.ExecNonQuery("RegistrationOfDetention", regist);
            }
        }

        public DetentionDto GetDetentionById(int id)
        {
            if (id != default(int))
            {
                var detention = context.ExecProcGetModel<Detention>("GetDetentionById", new SqlParameter("@DetentionId", id));

                if (detention != null)
                {
                    var ReleaseEmpl = context.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.ReleaseProceduresID),
                        new SqlParameter("@ExecuProcName", "Release"));

                    var DetainedEmpl = context.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.DetentionProceduresID),
                        new SqlParameter("@ExecuProcName", "Detention"));

                    var DeliveredEmpl = context.ExecProcGetModel<EmployeeDto>("GetEmployeeByExecutionProcedureID",
                        new SqlParameter("@ExecProcedureID", detention.DeliveredProceduresID),
                        new SqlParameter("@ExecuProcName", "Delivery"));

                    var detentionDto = new DetentionDto()
                    {
                        DetentionID = detention.DetentionID,
                        PrisonerID = detention.PrisonerID,
                        PlaceofDetention = detention.PlaceofDetention,
                        DateOfDetention = detention.DateOfDetention,
                        DateOfArrival = detention.DateOfArrival,
                        DateOfRelease = detention.DateOfRelease,
                        AccruedAmount = detention.AccruedAmount,
                        DeliveredEmployee = DeliveredEmpl,
                        DetainedEmployee = DetainedEmpl,
                        ReleasedEmployee = ReleaseEmpl,
                        PaidAmount = detention.PaidAmount
                    };

                    return detentionDto;
                }
            }
            return default(DetentionDto);
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisonerDto release)
        {
            if (release != null)
            {
                context.ExecNonQuery("insertEmployee",
                         release.ReleasedEmployee,
                        "employeeID", out int _ReleasedEmployeeID);

                context.ExecNonQuery("InsertReleaseProcedures",
                      "ReleaseProceduresID",
                      out int _ReleasedProceduresID,
                      new SqlParameter("@employeeID", _ReleasedEmployeeID));

                var ReleaseOfPrisoner = new ReleaseOfPrisoner()
                {
                    AccruedAmount = release.AccruedAmount,
                    DateOfRelease = release.DateOfRelease,
                    DetentionID = release.DetentionID,
                    PaidAmount = release.PaidAmount,
                    ReleaseProceduresID = _ReleasedProceduresID
                };

                context.ExecNonQuery("ReleaseOfPrisoner", ReleaseOfPrisoner);
            }
        }

        public void EditDetention(DetentionDto detention)
        {
            if (detention != null)
            {
                var detentionEntity = new Detention()
                {
                    DetentionID = detention.DetentionID,
                    AccruedAmount = detention.AccruedAmount ?? default(decimal),
                    PaidAmount = detention.PaidAmount ?? default(decimal),
                    DateOfArrival = detention.DateOfArrival,
                    DateOfDetention = detention.DateOfDetention,
                    DateOfRelease = detention.DateOfRelease,
                    PlaceofDetention = detention.PlaceofDetention
                };
                context.ExecNonQuery("EditDetention", detentionEntity);

                context.ExecNonQuery("EditEmployee", detention.DeliveredEmployee);
                context.ExecNonQuery("EditEmployee", detention.DetainedEmployee);
                if (detention.ReleasedEmployee != null)
                {
                    context.ExecNonQuery("EditEmployee", detention.ReleasedEmployee);
                }
            }
        }
    }
}
