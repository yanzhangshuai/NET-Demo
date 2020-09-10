namespace Blog.Core.Model.interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        ///    是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}