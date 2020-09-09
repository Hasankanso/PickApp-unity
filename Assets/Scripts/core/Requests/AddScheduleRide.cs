using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

namespace Requests {
    class AddScheduleRide : Request<ScheduleRide> {
        private ScheduleRide scheduleRide;

        public AddScheduleRide(ScheduleRide scheduleRide) {
            this.scheduleRide = scheduleRide;
            HttpPath = "";
        }

        public override ScheduleRide BuildResponse(JToken response) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject scheduleRideJ = scheduleRide.ToJson();
            return scheduleRideJ.ToString();
        }

        protected override string IsValid() {
            if (DateTime.Compare(scheduleRide.StartDate,DateTime.Now)<0) {
                return "Your schedule must start from today or later";
            }
            if (DateTime.Compare(scheduleRide.StartDate, scheduleRide.EndDate) < 0) {
                return "Start date must be before end date";
            }
            if (DateTime.Compare(scheduleRide.StartDate, scheduleRide.EndDate) > 0) {
                return "End date must be later than start date";
            }
            if ((scheduleRide.EndDate-scheduleRide.StartDate).TotalDays>30) {
                return "Schedule ride can be added for one month at maximum";
            }
            bool isAtLeastOneDay = false;
            if (scheduleRide.IsMonday|| scheduleRide.IsTuesday|| scheduleRide.IsWednesday || scheduleRide.IsThursday || scheduleRide.IsFriday || scheduleRide.IsSaturday || scheduleRide.IsSunday) {
                isAtLeastOneDay = true;
            }
            if (isAtLeastOneDay==false) {
                return "Choose at least one day of week";
            }

            return string.Empty;
        }
    }
}
