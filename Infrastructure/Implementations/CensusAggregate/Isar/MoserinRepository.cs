using Domain.Entities.CensusAggregate.Isar.Moserin;
using Domain.Interfaces.CensusAggregate.Isar;

namespace Infrastructure.Implementations.CensusAggregate.Isar;

public class MoserinRepository : IMoserinRepository
{
    IEnumerable<dynamic> IMoserinRepository.CreateMoserin(CreateMoserinModel createMoserinModel)
    {
        throw new NotImplementedException();
    }

    bool IMoserinRepository.DeleteMoserin(long moserId)
    {
        throw new NotImplementedException();
    }

    IEnumerable<dynamic> IMoserinRepository.GetAllMoserin()
    {
        throw new NotImplementedException();
    }

    GetMoserinModel IMoserinRepository.GetMoserin(long moserId)
    {
        throw new NotImplementedException();
    }

    IEnumerable<dynamic> IMoserinRepository.UpdateMoserin(UpdateMoserinModel updateMoserinModel)
    {
        throw new NotImplementedException();
    }
}
