﻿using System;
using System.Collections.Generic;
using NakamaNetwork.Entities.EnumTypes;
using TreasureGuide.Entities;

namespace NakamaNetwork.Entities.Models
{
    public partial class Donation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }
        public PaymentState State { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentId { get; set; }
        public string PayerId { get; set; }
        public string TokenId { get; set; }
        public bool? Public { get; set; }

        public virtual UserProfile User { get; set; }
    }
}
