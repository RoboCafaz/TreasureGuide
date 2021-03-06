﻿using System;
using TreasureGuide.Entities;

namespace TreasureGuide.Common.Models.NotificationModels
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public NotificationEventType EventType { get; set; }
        public DateTimeOffset ReceivedDate { get; set; }
    }
}