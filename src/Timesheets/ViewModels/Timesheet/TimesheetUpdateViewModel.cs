﻿using System;
using System.ComponentModel.DataAnnotations;
using Timesheets.Data.Entities;

namespace Timesheets.ViewModels.Timesheet
{
    public class TimesheetUpdateViewModel
    {
        public TimesheetUpdateViewModel() { }

        private readonly Data.Entities.Timesheet _entity;

        public DateTimeOffset TimesheetDate { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int TotalTimeByTask { get; set; }
        public int TotalTimeToday { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int SprintId { get; set; }
        [Required]
        public int TaskId { get; set; }


        internal Data.Entities.Timesheet ToEntity(Data.Entities.Timesheet entity, string username)
        {
            entity.TimesheetDate = this.TimesheetDate;
            entity.StartTime = this.StartTime;
            entity.EndTime = this.EndTime;
            entity.TotalTimeByTask = this.TotalTimeByTask;
            entity.TotalTimeToday = this.TotalTimeToday;
            entity.EmployeeId = this.EmployeeId;
            entity.SprintId = this.SprintId;
            entity.TaskId = this.TaskId;
            entity.Modified = DateTime.Now;
            entity.ModifiedBy = username;

            return entity;
        }
    }
}
