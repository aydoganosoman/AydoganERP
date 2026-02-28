namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class TagGroupDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<TagDto> Tags { get; set; } = new();
}
