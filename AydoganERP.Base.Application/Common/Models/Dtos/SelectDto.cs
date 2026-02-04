namespace AydoganERP.Base.Application.Common.Models.Dtos;

public class SelectDto<T>
{
    public T Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}