using System;
using System.Linq.Expressions;

namespace ExpressionExam
{
    class Program
    {
        static void Main(string[] args)
        {

            ParameterExpression param2 = Expression.Parameter(typeof(int));

            LabelTarget labelTarget= Expression.Label(typeof(Int32));
            BlockExpression block2 = Expression.Block
            (
                    new[] { param2 },
                    Expression.Assign(param2, Expression.Constant(10)),
                    Expression.AddAssign(param2, Expression.Constant(20))
            );
            Expression<Func<int>> expr2 = Expression.Lambda<Func<int>>(block2);
            Console.WriteLine(expr2.Compile().Invoke());


            //LabelTarget labelBreak = Expression.Label();  测试
            //ParameterExpression loopIndex = Expression.Parameter(typeof(int), "index");

            //BlockExpression block = Expression.Block
            //(
            //    new[] { loopIndex },
            //    // 初始化loopIndex =1
            //    Expression.Assign(loopIndex, Expression.Constant(1)),
            //    Expression.Loop
            //    (
            //        Expression.IfThenElse
            //        (
            //            // if 的判断逻辑
            //            Expression.LessThanOrEqual(loopIndex, Expression.Constant(10)),
            //            // 判断逻辑通过的代码
            //            Expression.Block
            //            (
            //                Expression.Call
            //                (
            //                    null,
            //                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
            //                    Expression.Constant("Hello")
            //                 ),
            //                Expression.PostIncrementAssign(loopIndex)
            //             ),
            //            // 判断不通过的代码
            //            Expression.Break(labelBreak)
            //          ), labelBreak
            //      )
            //   );

            //// 将我们上面的代码块表达式
            //Expression<Action> lambdaExpression = Expression.Lambda<Action>(block);
            //lambdaExpression.Compile().Invoke();

            Console.ReadLine();
        }
    }
}
