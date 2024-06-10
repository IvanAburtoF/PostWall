using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostWall.API.Models.DTO;
using PostWall.API.Services;

namespace PostWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateCommentAsync(CommentDTO commentDTO)
        {
            try
            {
                var comment = await _commentService.CreateCommentAsync(commentDTO);
                return CreatedAtAction(nameof(GetCommentByIdAsync), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentByIdAsync(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsAsync()
        {
            try
            {
                var comments = await _commentService.GetCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
