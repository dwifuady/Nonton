namespace Nonton.Features.Meta;
public interface IMetaService
{
    Task<Models.IMeta> GetMeta(string metaType, string id);
}
