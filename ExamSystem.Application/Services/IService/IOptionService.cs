using ExamSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.IService
{
    public interface IOptionService
    {
        bool CreateOptions(OptionDto optionDto, int questionId);
    }
}
