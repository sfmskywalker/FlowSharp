namespace Elsa.Secrets.Management;

public class SecretFilter
{
    public string? Id { get; set; }
    public ICollection<string>? Ids { get; set; }
    public string? Name { get; set; }
    public int? Version { get; set; }
    public string? Type { get; set; }
    public SecretStatus? Status { get; set; }
    public string? EncryptionKeyId { get; set; }
    public string? SearchTerm { get; set; }

    public IQueryable<Secret> Apply(IQueryable<Secret> queryable)
    {
        if (Id != null) queryable = queryable.Where(x => x.Id == Id);
        if (Ids != null) queryable = queryable.Where(x => Ids.Contains(x.Id));
        if (Name != null) queryable = queryable.Where(x => x.Name == Name);
        if (Version != null) queryable = queryable.Where(x => x.Version == Version);
        if (Type != null) queryable = queryable.Where(x => x.Type == Type);
        if(Status != null) queryable = queryable.Where(x => x.Status == Status);
        if (EncryptionKeyId != null) queryable = queryable.Where(x => x.EncryptionKeyId == EncryptionKeyId);
        if (!string.IsNullOrWhiteSpace(SearchTerm)) queryable = queryable.Where(x => x.Name.Contains(SearchTerm) || x.Description.Contains(SearchTerm) || x.Id.Contains(SearchTerm));

        return queryable;
    }
}