using Data.DTOs.ChildDtos;

namespace Business.Services.ChildServices
{
    public interface IChildService
    {
        Task<IEnumerable<ChildDto>> GetAllChildrenAsync();
        Task<ChildDto> GetChildAsync(int id);
        Task<ChildDto> CreateChildAsync(ChildCreateDto childCreateDto);
        Task<ChildDto> UpdateChildAsync(int id, ChildDto childDto);
        Task<bool> DeleteChildAsync(int id);

        Task<IEnumerable<ChildDto>> SearchChildrenByDifficultyAsync(string difficulty);
        Task<IEnumerable<ChildDto>> SearchChildrenByParentAsync(string parentName);

    }
}
