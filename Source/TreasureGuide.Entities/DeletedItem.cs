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
    
    public partial class DeletedItem
    {
        public int Id { get; set; }
        public DeletedItemType Type { get; set; }
        public System.DateTimeOffset EditedDate { get; set; }
    }
}