using Dapper;
using Sample02.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sample02
{
    public class DBTest
    {
        public const string DbConnectionString = @"Data Source=127.0.0.1;User ID=sa;Password=longruan@123;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;";

        public static void InitData()
        {
            var content = new Content
            {
                title = "标题 001",
                content = "内容 001"
            };
            var comments = new List<Comment> {
                new Comment
                {
                    content_id = 4,
                    content = "评论001"
                },
                new Comment
                {
                    content = "评论002",
                    content_id = 4
                }
            };
            using (var conn = new SqlConnection(DbConnectionString))
            {
                string cmdText = @"INSERT INTO [Content]
                        (title, [content], status, add_time, modify_time)
                        VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(cmdText, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
                cmdText = @"INSERT INTO [Comment]
                        (content_id, [content], add_time)
                        VALUES   (@content_id,@content,@add_time)";
                result = conn.Execute(cmdText, comments);
                 Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        public static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection(DbConnectionString))
            {
                string sql_insert = @"select * from content where id=@id;
select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 4 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithCommnet>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }

            }
        }
    }
}
