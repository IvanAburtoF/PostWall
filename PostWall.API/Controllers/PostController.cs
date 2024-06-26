using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Services;
using System.Security.Claims;
namespace PostWall.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IUserService _userService;

    public PostController(IPostService postService, IUserService userService)
    {
        _postService = postService;
        _userService = userService;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDetailsDTO>> CreatePost(CreatePostDTO postDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var userID = _userService.GetCurrentUserId();
            if (userID == null)
            {
                return Unauthorized();
            }
            var post = await _postService.CreatePostAsync(postDTO, userID);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet ("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDetailsDTO>> GetPostById(int id)
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PostListDTO>>> GetPosts()
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
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDetailsDTO>> UpdatePost(UpdatePostDTO postDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var userID = _userService.GetCurrentUserId();
            if (userID == null)
            {
                return Unauthorized();
            }
            var post = await _postService.UpdatePostAsync(postDTO, userID);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
    public async Task<ActionResult> DeletePost(int id)
    {
        try
        {
            var userID = _userService.GetCurrentUserId();
            if (userID == null)
            {
                return Unauthorized();
            }
            await _postService.DeletePostAsync(id, userID);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize]
    [HttpPost("{postId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> LikePost(int postId)
    {
        try
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null)
            {
                return Unauthorized();
            }
            await _postService.LikePostAsync(postId, userID);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize]
    [HttpPost("{postId}/dislike")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DislikePost(int postId)
    {
        try
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null)
            {
                return Unauthorized();
            }
            await _postService.DislikePostAsync(postId, userID);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
