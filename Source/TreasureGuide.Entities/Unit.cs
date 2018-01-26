//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
            this.RoundUnits = new HashSet<RoundUnit>();
            this.TeamUnits = new HashSet<TeamUnit>();
            this.UnitAliases = new HashSet<UnitAlias>();
            this.Stages = new HashSet<Stage>();
            this.TeamUnitSummaries = new HashSet<TeamUnitSummary>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitType Type { get; set; }
        public UnitClass Class { get; set; }
        public Nullable<byte> Stars { get; set; }
        public Nullable<byte> Cost { get; set; }
        public Nullable<byte> Combo { get; set; }
        public Nullable<byte> Sockets { get; set; }
        public Nullable<byte> MaxLevel { get; set; }
        public Nullable<int> EXPtoMax { get; set; }
        public Nullable<short> MinHP { get; set; }
        public Nullable<short> MinATK { get; set; }
        public Nullable<short> MinRCV { get; set; }
        public Nullable<short> MaxHP { get; set; }
        public Nullable<short> MaxATK { get; set; }
        public Nullable<short> MaxRCV { get; set; }
        public Nullable<decimal> GrowthRate { get; set; }
        public UnitFlag Flags { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoundUnit> RoundUnits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamUnit> TeamUnits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnitAlias> UnitAliases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stage> Stages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamUnitSummary> TeamUnitSummaries { get; set; }
    }
}
