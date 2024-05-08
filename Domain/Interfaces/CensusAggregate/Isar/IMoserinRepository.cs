namespace Domain.Interfaces.CensusAggregate.Isar;

public interface IMoserinRepository
{
    IEnumerable<dynamic> CreateMoserin(CreateMoserinModel createMoserinModel);
    IEnumerable<dynamic> UpdateMoserin(UpdateMoserinModel updateMoserinModel);
    GetMoserinModel GetMoserin(long moserId);
    IEnumerable<dynamic> GetAllMoserin();
    bool DeleteMoserin(long moserId);
}
