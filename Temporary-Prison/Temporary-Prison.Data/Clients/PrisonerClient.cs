﻿using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;


namespace Temporary_Prison.Data.Clients
{
    public class PrisonerClient : IPrisonerClient
    {
        public PrisonerDto GetPrisonerById(int Id)
        {
            return new PrisonerServiceClient().Execute(client => client.GetPrisonerById(Id));
        }

        public IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
        {
            int totalCountPrisoners = default(int);

            var prisoners = new PrisonerServiceClient().Execute(client =>
            client.GetPrisonersForPagedList(skip, rowSize, out totalCountPrisoners));

            totalCount = totalCountPrisoners;

            return prisoners;
        }


        public bool AddPrisoner(PrisonerDto prisoner, out int newId)
        {
            int _newId = default(int);
            var result = new PrisonerServiceClient().Execute(client =>
            client.AddPrisoner(prisoner, out _newId));
            newId = _newId;
            return result;
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {
            new PrisonerServiceClient().Execute(client =>
            client.RegisterDetention(registrationOfDetention));
        }



        public IReadOnlyList<PrisonerDto> FindPrisonersByName(string search)
        {
            return new PrisonerServiceClient().Execute(clinet => clinet.FindPrisonersByName(search));
        }
    }
}
