using System;
using System.IO;
using System.Threading.Tasks;

public interface ISave
{
	Task SaveAndView(DateTime datefrom, DateTime dateto, string parameter);
}

