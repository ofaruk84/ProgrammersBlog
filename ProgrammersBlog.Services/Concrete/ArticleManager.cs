using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDto>> GetById(int id)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == id, a => a.User, a => a.Category);

            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<ArticleDto>(ResultStatus.Error, "There s no such a article", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, "There s no such a article", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNoneDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => a.IsDeleted == false, a => a.User, a => a.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, "There s no such a article", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNoneDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => a.IsDeleted == false && a.IsActive, a => a.User, a => a.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, "There s no such a article", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);

            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId, a => a.User, a => a.Category);

                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });

                }

                return new DataResult<ArticleListDto>(ResultStatus.Error, "There s no such a article", null);
            }

            return new DataResult<ArticleListDto>(ResultStatus.Error, "There s no such a category", null);
        }

        public async Task<IDataResult<ArticleDto>> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;

            var addedArticle = await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();

            return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleAddDto.Title} has been successfully added", new ArticleDto
            {
                Article = addedArticle,
                ResultStatus = ResultStatus.Success,
                Message = $"{articleAddDto.Title} has been successfully added"
            });
        }

        public async Task<IDataResult<ArticleDto>> Update(ArticleUpdateDto articleUpdatedDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdatedDto);
            article.ModifiedByName = modifiedByName;


            var updatedArticle = await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleUpdatedDto.Title} has been successfully updated", new ArticleDto
            {
                Article = updatedArticle,
                ResultStatus = ResultStatus.Success,
                Message = $"{articleUpdatedDto.Title} has been successfully updated"
            });
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {

            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedDate = DateTime.Now;
                article.ModifiedByName = modifiedByName;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, "Article  has been deleted");

            }

            return new Result(ResultStatus.Error, "Article  can not be  found");
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);

                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Article  has been deleted from database");
            }

            return new Result(ResultStatus.Error, "Article  can not be found");
        }
    }
}
