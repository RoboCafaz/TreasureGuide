﻿using System.Collections.Generic;
using TreasureGuide.Entities.Interfaces;

namespace TreasureGuide.Web.Models.TeamModels
{
    public class TeamStubModel : IIdItem<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubmittedById { get; set; }
        public string SubmittedByName { get; set; }
        public int Score { get; set; }
        public bool Global { get; set; }

        public IEnumerable<int> TeamUnits { get; set; }
    }

    public class TeamDetailModel : IIdItem<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubmittedById { get; set; }
        public string SubmittedByName { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public string Guide { get; set; }
        public string Credits { get; set; }
        public bool Global { get; set; }

        public IEnumerable<TeamUnitDetailModel> TeamUnits { get; set; }
        public IEnumerable<TeamSocketStubModel> TeamSockets { get; set; }
    }

    public class TeamEditorModel : IIdItem<int?>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Credits { get; set; }
        public string Guide { get; set; }
        public IEnumerable<TeamSocketEditorModel> TeamSockets { get; set; }
        public IEnumerable<TeamUnitEditorModel> TeamUnits { get; set; }
    }
}
