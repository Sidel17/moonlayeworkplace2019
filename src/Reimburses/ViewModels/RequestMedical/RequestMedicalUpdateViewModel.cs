﻿using System;
using Microsoft.AspNetCore.Http;
using Reimburses.Data.Entities;

namespace Reimburses.ViewModels.RequestMedical
{
    public class RequestMedicalUpdateViewModel
    {
        public RequestMedicalUpdateViewModel() { }
    //    private readonly Data.Entities.RequestMedical _entity;
        public DateTimeOffset DateRequestMedical { get; set; }
        public string MedicationType { get; set; }
        public int TotalCostNominal { get; set; }
        public int TotalCostReimburse { get; set; }
        public IFormFile Image { get; set; }
        internal Data.Entities.RequestMedical ToRequestMedicalEntity(Data.Entities.RequestMedical entity, string username)
        {
            entity.DateRequestMedical = this.DateRequestMedical;
            entity.MedicationType = this.MedicationType;
            entity.TotalCostNominal = this.TotalCostNominal;
            entity.TotalCostReimburse = this.TotalCostReimburse;
            entity.Modified = DateTime.Now;
            entity.ModifiedBy = username;

            return entity;
        }
    }
}
