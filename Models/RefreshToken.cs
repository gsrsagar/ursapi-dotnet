using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace urs_api.Models
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Created { get; set; }
        [AllowNull]
        public string? CreatedByIp { get; set; }
        [AllowNull]
        public DateTime? Revoked { get; set; }
        [AllowNull]
        public string? RevokedByIp { get; set; }
        [AllowNull]
        public string? ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}