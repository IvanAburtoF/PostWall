using Microsoft.AspNetCore.Mvc;
using PostWall.API.Models.DTO;
using PostWall.API.Services;
namespace PostWall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [HttpPost]

    public async Task<ActionResult<PostDTO>> CreatePostAsync(PostDTO postDTO)
    {
        var post = await _postService.CreatePostAsync(postDTO);
        return CreatedAtAction(nameof(GetPostByIdAsync), new { id = post.Id }, post);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDTO>> GetPostByIdAsync(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        return Ok(post);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsAsync()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }
    [HttpPut]
    public async Task<ActionResult<PostDTO>> UpdatePostAsync(PostDTO postDTO)
    {
        var post = await _postService.UpdatePostAsync(postDTO);
        return Ok(post);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePostAsync(int id)
    {
        await _postService.DeletePostAsync(id);
        return NoContent();
    }
}
