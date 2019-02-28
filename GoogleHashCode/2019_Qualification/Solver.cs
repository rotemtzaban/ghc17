using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(0, false, null) }));
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(3, false, null) }));
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(1, true, null), new Photo(2, true, null) }));

            return res;
        }
    }
}
