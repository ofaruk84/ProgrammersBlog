using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Category>> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == id, c => c.Articles);

            if (category != null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }

            return new DataResult<Category>(ResultStatus.Error, null);
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null,c => c.Articles);

            if (categories.Count >-1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }

            return new DataResult<IList<Category>>(ResultStatus.Success,"Categories Can Not Be Found", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNoneDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c=>c.IsDeleted==false, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }

            return new DataResult<IList<Category>>(ResultStatus.Success, "Categories Can Not Be Found", null);
        }
    

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
           await _unitOfWork.Categories.AddAsync(new Category
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Description,
                IsActive = categoryAddDto.IsActive,
                CreatedByName = createdByName,
                CreatedDate = DateTime.Now,
                ModifiedByName = createdByName,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            }).ContinueWith(t=>_unitOfWork.SaveAsync());

           

           return new Result(ResultStatus.Success,$"{categoryAddDto.Name} has been successfully added");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);

            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.Note = categoryUpdateDto.Note;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, "Updated Successfully");
            }

            return new Result(ResultStatus.Error, "Category cant be updated");
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} has been successfully deleted");
            }

            return new Result(ResultStatus.Success, $"{category.Name} can not be deleted");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} has been successfully deleted from database");
            }

            return new Result(ResultStatus.Success, $"{category.Name} can not be deleted");
        }
    }
}
