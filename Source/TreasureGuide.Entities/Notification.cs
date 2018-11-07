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
    
    public partial class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public NotificationEventType EventType { get; set; }
        public Nullable<int> EventId { get; set; }
        public string TriggerUserId { get; set; }
        public System.DateTimeOffset ReceivedDate { get; set; }
    
        public virtual UserProfile TargetUser { get; set; }
        public virtual UserProfile TriggeringUser { get; set; }
    }
}