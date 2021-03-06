using System;
using Blog.Core.Model.interfaces;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public  class Advertisement:ISoftDelete,IEntity<int>
    {

        /// <summary>
        /// 广告图片
        /// </summary>
        
        public string ImgUrl { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        
        public string Title { get; set; }

        /// <summary>
        /// 广告链接
        /// </summary>
       
        public string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
       
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createdate { get; set; } = DateTime.Now;

        /// <summary>
        ///     是否删除
        /// </summary>
        public bool IsDeleted { get; set; }


        public int Id { get; set; }
    }
}