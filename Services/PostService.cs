namespace WebApi.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Posts;
using WebApi.Views;

public interface IPostService
{
    IEnumerable<PostItem> GetAll();
    PostItem GetById(int id);
    void Post(PostRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
    IEnumerable<PostDetailView> GetDetailedPostsForUserById(int userId);
}

public class PostService : IPostService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public PostService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<PostItem> GetAll()
    {
        return _context.PostItems;
    }

    public PostItem GetById(int id)
    {
        return GetPost(id);
    }

    public void Post(PostRequest model)
    {
        // validate
        if (model.Content.Length < 1)
            throw new AppException("Post '" + model.Content + "' contains no text");

        // map model to new event object
        var postItem = _mapper.Map<PostItem>(model);
        postItem.Author = _context.Users.FirstOrDefault(x => x.Id == model.AuthorID);

        // save event
        _context.PostItems.Add(postItem);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var postItem = GetPost(id);

        // validate
        if (model.Content.Length < 1)
            throw new AppException("Post '" + model.Content + "' contains no text");

        // copy model to event and save
        _mapper.Map(model, postItem);
        _context.PostItems.Update(postItem);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var postItem = GetPost(id);
        _context.PostItems.Remove(postItem);
        _context.SaveChanges();
    }

    public IEnumerable<PostDetailView> GetDetailedPostsForUserById(int userId)
    {
        return (from post in _context.PostItems
                     where post.Author.Id == userId
                     select new PostDetailView
                     {
                         AuthorId = post.Author.Id,
                         AuthorFirstName = post.Author.FirstName,
                         AuthorLastName = post.Author.LastName,
                         AuthorUserName = post.Author.Username,
                         PostId = post.Id,
                         PostContent = post.Content,
                         PublishDate = post.PublishDate,
                     });
    }

    // helper methods

    private PostItem GetPost(int id)
    {
        var postItem = _context.PostItems.Find(id);
        if (postItem == null) throw new KeyNotFoundException("Event not found");
        return postItem;
    }

}