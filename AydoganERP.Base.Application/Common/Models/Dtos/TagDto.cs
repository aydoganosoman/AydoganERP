namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class TagDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid TagGroupId { get; set; }
    public string? TagGroupName { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}
