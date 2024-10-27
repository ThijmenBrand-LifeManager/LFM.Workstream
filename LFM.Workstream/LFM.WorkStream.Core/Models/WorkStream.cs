using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LFM.WorkStream.Core.Enums;

namespace LFM.WorkStream.Core.Models;

[Table("WorkStreams")]
public class WorkStream
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public WorkstreamState State { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset CreatedAt { get; }

    public ICollection<Project> Projects { get; } = new List<Project>();
}