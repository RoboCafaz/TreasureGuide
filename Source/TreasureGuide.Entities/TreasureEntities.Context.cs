﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TreasureGuide.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TreasureEntities : DbContext
    {
        public TreasureEntities()
            : base("name=TreasureEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TeamSocket> TeamSockets { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<RoundUnitBehavior> RoundUnitBehaviors { get; set; }
        public virtual DbSet<RoundUnit> RoundUnits { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<TeamUnit> TeamUnits { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamVote> TeamVotes { get; set; }
        public virtual DbSet<TeamReport> TeamReports { get; set; }
        public virtual DbSet<TeamVideo> TeamVideos { get; set; }
        public virtual DbSet<TeamCredit> TeamCredits { get; set; }
        public virtual DbSet<UnitAlias> UnitAliases { get; set; }
        public virtual DbSet<TeamUnitSummary> TeamUnitSummaries { get; set; }
        public virtual DbSet<TeamGenericSlot> TeamGenericSlots { get; set; }
        public virtual DbSet<Box> Boxes { get; set; }
        public virtual DbSet<UserPreference> UserPreferences { get; set; }
    
        [DbFunction("TreasureEntities", "SimilarTeams")]
        public virtual IQueryable<SimilarTeams_Result> SimilarTeams(Nullable<int> teamId, Nullable<int> stageId, Nullable<int> unit1, Nullable<int> unit2, Nullable<int> unit3, Nullable<int> unit4, Nullable<int> unit5, Nullable<int> unit6)
        {
            var teamIdParameter = teamId.HasValue ?
                new ObjectParameter("teamId", teamId) :
                new ObjectParameter("teamId", typeof(int));
    
            var stageIdParameter = stageId.HasValue ?
                new ObjectParameter("stageId", stageId) :
                new ObjectParameter("stageId", typeof(int));
    
            var unit1Parameter = unit1.HasValue ?
                new ObjectParameter("unit1", unit1) :
                new ObjectParameter("unit1", typeof(int));
    
            var unit2Parameter = unit2.HasValue ?
                new ObjectParameter("unit2", unit2) :
                new ObjectParameter("unit2", typeof(int));
    
            var unit3Parameter = unit3.HasValue ?
                new ObjectParameter("unit3", unit3) :
                new ObjectParameter("unit3", typeof(int));
    
            var unit4Parameter = unit4.HasValue ?
                new ObjectParameter("unit4", unit4) :
                new ObjectParameter("unit4", typeof(int));
    
            var unit5Parameter = unit5.HasValue ?
                new ObjectParameter("unit5", unit5) :
                new ObjectParameter("unit5", typeof(int));
    
            var unit6Parameter = unit6.HasValue ?
                new ObjectParameter("unit6", unit6) :
                new ObjectParameter("unit6", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SimilarTeams_Result>("[TreasureEntities].[SimilarTeams](@teamId, @stageId, @unit1, @unit2, @unit3, @unit4, @unit5, @unit6)", teamIdParameter, stageIdParameter, unit1Parameter, unit2Parameter, unit3Parameter, unit4Parameter, unit5Parameter, unit6Parameter);
        }
    
        [DbFunction("TreasureEntities", "SimilarTeamsId")]
        public virtual IQueryable<SimilarTeams_Result> SimilarTeamsId(Nullable<int> teamId)
        {
            var teamIdParameter = teamId.HasValue ?
                new ObjectParameter("teamId", teamId) :
                new ObjectParameter("teamId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SimilarTeams_Result>("[TreasureEntities].[SimilarTeamsId](@teamId)", teamIdParameter);
        }
    }
}
