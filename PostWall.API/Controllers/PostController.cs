using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Services;
using System.Security.Claims;
namespace PostWall.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<ActionResult<PostDetailsDTO>> CreatePostAsync(CreatePostDTO postDTO)
    {
        try
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null)
            {
                return Unauthorized();
            }
            var post = await _postService.CreatePostAsync(postDTO, userID);
            return CreatedAtAction(nameof(GetPostByIdAsync), new { id = post.Id }, post);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDetailsDTO>> GetPostByIdAsync(int id)
    {
        try
        {
            var post = await _postService.GetPostByIdAsync(id);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostListDTO>>> GetPostsAsync()
    {
        try
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPut]
    public async Task<ActionResult<PostDetailsDTO>> UpdatePostAsync(UpdatePostDTO postDTO)
    {
        try
        {
            var post = await _postService.UpdatePostAsync(postDTO);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePostAsync(int id)
    {
        try
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
