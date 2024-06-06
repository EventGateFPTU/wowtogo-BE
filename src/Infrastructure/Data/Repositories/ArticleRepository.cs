using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class ArticleRepository(WowToGoDBContext context) : RepositoryBase<Article>(context), IArticleRepository
{

}