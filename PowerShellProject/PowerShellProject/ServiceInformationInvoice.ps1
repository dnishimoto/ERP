function OutputFile($path,$fileName,$fileAndPath,$content)
{
	$result=Test-Path $fileAndPath -PathType leaf
	if($result -eq $true)
	{
		Remove-Item –Path $fileAndPath
	}
	Write-Host "File: $fileName '..." -foregroundColor "Magenta"
	New-Item -Path $path -Name $fileName -ItemType "file" -Value $content
}

$current_location=Get-Location;

$path=$current_location.Path +"\tmp_script";

$result=Test-Path $path -PathType Container

if ($result -eq $false)
{
New-Item -ItemType directory -Path $path
}

$class="ServiceInformationInvoice"; ###_xx_
$concrete="serviceInformationInvoice"; ###_yy_
$domain="ServiceInformationInvoiceDomain"; ##_dd_
$modulename="ServiceInformationInvoice"; ###_zz_

#################################################################################

$content0="

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2._dd_
{
public interface I_xx_Repository
    {
        Task<_xx_> GetEntityById(long ? _yy_Id);
	    Task<_xx_> FindEntityByExpression(Expression<Func<_xx_, bool>> predicate);
		Task<IList<_xx_>> FindEntitiesByExpression(Expression<Func<_xx_, bool>> predicate);
    }
}
";

$content0=$content0 -replace "_xx_", $class;
$content0=$content0 -replace "_yy_", $concrete;
$content0=$content0 -replace "_dd_", $domain;

$content0

$fileName0="I_xx_Repository.cs"
$fileName0=$fileName0 -replace "_xx_", $class

$fileName0

$fileAndPath0=($path + "\" + $fileName0)

OutputFile $path $fileName0 $fileAndPath0 $content0
#################################################################################

$content1="   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2._dd_
{
 public class _xx_Repository: Repository<_xx_>, I_xx_Repository
    {
        ListensoftwaredbContext _dbContext;
        public _xx_Repository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<_xx_>GetEntityById(long ? _yy_Id)
        {
			try{
            return await _dbContext.FindAsync<_xx_>(_yy_Id);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<_xx_> GetEntityByNumber(long _yy_Number)
        {
			try
			{
            var query = await (from detail in _dbContext._xx_
                               where detail._xx_Number == _yy_Number
                               select detail).FirstOrDefaultAsync<_xx_>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<_xx_> FindEntityByExpression(Expression<Func<_xx_, bool>> predicate)
        {
            IQueryable<_xx_> result = _dbContext.Set<_xx_>().Where(predicate);

            return await result.FirstOrDefaultAsync<_xx_>();
        }
		  public async Task<IList<_xx_>> FindEntitiesByExpression(Expression<Func<_xx_, bool>> predicate)
        {
            IQueryable<_xx_> result = _dbContext.Set<_xx_>().Where(predicate);

            return await result.ToListAsync<_xx_>();
        }
		
  }
}
";

$content1=$content1 -replace "_xx_", $class;
$content1=$content1 -replace "_yy_", $concrete;
$content1=$content1 -replace "_dd_", $domain;

$content1

$fileName1="_xx_Repository.cs"
$fileName1=$fileName1 -replace "_xx_", $class

$fileName1

$fileAndPath1=($path + "\" + $fileName1)

OutputFile $path $fileName1 $fileAndPath1 $content1


#################################################################################
$content2="

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2._dd_;

namespace lssWebApi2._dd_
{ 

public interface IFluent_xx_
    {
        IFluent_xx_Query Query();
        IFluent_xx_ Apply();
        IFluent_xx_ Add_xx_(_xx_ _yy_);
        IFluent_xx_ Update_xx_(_xx_ _yy_);
        IFluent_xx_ Delete_xx_(_xx_ _yy_);
     	IFluent_xx_ Update_xx_s(List<_xx_> newObjects);
        IFluent_xx_ Add_xx_s(List<_xx_> newObjects);
        IFluent_xx_ Delete_xx_s(List<_xx_> deleteObjects);
    }
}
";

$content2=$content2 -replace "_xx_", $class;
$content2=$content2 -replace "_yy_", $concrete;
$content2=$content2 -replace "_dd_", $domain;

$content2

$fileName2="IFluent_xx_.cs"
$fileName2=$fileName2 -replace "_xx_", $class

$fileName2
$fileAndPath2=($path + "\" + $fileName2)


OutputFile $path $fileName2 $fileAndPath2 $content2

#################################################################################

$content3="using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2._dd_;


namespace lssWebApi2._dd_
{
 public interface IFluent_xx_Query
    {

	
Task<_xx_> MapToEntity(_xx_View inputObject);
    Task<List<_xx_>> MapToEntity(List<_xx_View> inputObjects);
    Task<_xx_View> MapToView(_xx_ inputObject);
    Task<NextNumber> GetNextNumber();
    Task<_xx_View> GetViewById(long ? _yy_Id);
    Task<_xx_> GetEntityById(long ? _yy_Id);
    Task<_xx_View> GetViewByNumber(long _yy_Number);
    Task<_xx_View> GetEntityByNumber(long _yy_Number);
 }
}
";

$content3=$content3 -replace "_xx_", $class;
$content3=$content3 -replace "_yy_",$concrete;
$content3=$content3 -replace "_dd_", $domain;

$content3

$fileName3="IFluent_xx_Query.cs"
$fileName3=$fileName3 -replace "_xx_", $class

$fileName3
$fileAndPath3=($path + "\" + $fileName3)


OutputFile $path $fileName3 $fileAndPath3 $content3

#################################################################################

$content4="using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2._dd_;
using lssWebApi2.Enumerations;

namespace lssWebApi2._dd_
{

public class Fluent_xx_ :IFluent_xx_
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public Fluent_xx_() { }
        public IFluent_xx_Query Query()
        {
            return new Fluent_xx_Query(unitOfWork) as IFluent_xx_Query;
        }
        public IFluent_xx_ Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluent_xx_;
		    }
            catch (Exception ex) { throw new Exception(""Apply"", ex); }
        }
        public IFluent_xx_ Add_xx_s(List<_xx_> newObjects)
        {
            unitOfWork._yy_Repository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluent_xx_;
        }
        public IFluent_xx_ Update_xx_s(List<_xx_> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork._yy_Repository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluent_xx_;
        }
        public IFluent_xx_ Add_xx_(_xx_ newObject) {
            unitOfWork._yy_Repository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluent_xx_;
        }
        public IFluent_xx_ Update_xx_(_xx_ updateObject) {
            unitOfWork._yy_Repository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluent_xx_;

        }
        public IFluent_xx_ Delete_xx_(_xx_ deleteObject) {
            unitOfWork._yy_Repository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluent_xx_;
        }
   	public IFluent_xx_ Delete_xx_s(List<_xx_> deleteObjects)
        {
            unitOfWork._yy_Repository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluent_xx_;
        }
    }
}
"

$content4=$content4 -replace "_xx_",$class;
$content4=$content4 -replace "_yy_",$concrete;
$content4=$content4 -replace "_dd_",$domain;

$fileName4="Fluent_xx_.cs"
$fileName4=$fileName4 -replace "_xx_", $class

$fileName4

$fileAndPath4=($path + "\" + $fileName4)

OutputFile $path $fileName4 $fileAndPath4 $content4

#################################################################################

$content5="using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;

namespace lssWebApi2._dd_
{
public class Fluent_xx_Query:MapperAbstract<_xx_,_xx_View>,IFluent_xx_Query
    {
        private UnitOfWork _unitOfWork = null;
        public Fluent_xx_Query() { }
        public Fluent_xx_Query(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<_xx_> MapToEntity(_xx_View inputObject)
        {
            _xx_ outObject = mapper.Map<_xx_>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<_xx_>> MapToEntity(List<_xx_View> inputObjects)
        {
            List<_xx_> list = new List<_xx_>();
            foreach (var item in inputObjects)
            {
                _xx_ outObject = mapper.Map<_xx_>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<_xx_View> MapToView(_xx_ inputObject)
        {
            _xx_View outObject = mapper.Map<_xx_View>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork._yy_Repository.GetNextNumber('_xx_Number');
        }
 public override async Task<_xx_View> GetViewById(long ? _yy_Id)
        {
            _xx_ detailItem = await _unitOfWork._yy_Repository.GetEntityById(_yy_Id);

            return await MapToView(detailItem);
        }
 public async Task<_xx_View> GetViewByNumber(long _yy_Number)
        {
            _xx_ detailItem = await _unitOfWork._yy_Repository.GetEntityByNumber(_yy_Number);

            return await MapToView(detailItem);
        }

public override async Task<_xx_> GetEntityById(long ? _yy_Id)
        {
            return await _unitOfWork._yy_Repository.GetEntityById(_yy_Id);

        }
 public async Task<_xx_> GetEntityByNumber(long _yy_Number)
        {
            return await _unitOfWork._yy_Repository.GetEntityByNumber(_yy_Number);
        }
}
}
";


$content5=$content5 -replace "_xx_", $class;
$content5=$content5 -replace "_yy_", $concrete;
$content5=$content5 -replace "_dd_", $domain;

$content5

$fileName5="Fluent_xx_Query.cs"
$fileName5=$fileName5 -replace "_xx_", $class

$fileName5

$fileAndPath5=($path + "\" + $fileName5)

OutputFile $path $fileName5 $fileAndPath5 $content5
#################################################################################

$content6="using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluent_xx_Query
{
        Task<_xx_> MapToEntity(_xx_View inputObject);
        Task<List<_xx_>> MapToEntity(List<_xx_View> inputObjects);
    
        Task<_xx_View> MapToView(_xx_ inputObject);
        Task<NextNumber> GetNextNumber();
	Task<_xx_> GetEntityById(long ? _yy_Id);
	  Task<_xx_> GetEntityByNumber(long _yy_Number);
	Task<_xx_View> GetViewById(long ? _yy_Id);
	Task<_xx_View> GetViewByNumber(long _yy_Number);
}
"

$content6=$content6 -replace "_xx_", $class;
$content6=$content6 -replace "_yy_", $concrete;
$content6=$content6 -replace "_dd_", $domain;

$content6

$fileName6="IFluent_xx_Query.cs"
$fileName6=$fileName6 -replace "_xx_", $class

$fileName6

$fileAndPath6=($path + "\" + $fileName6)

OutputFile $path $fileName6 $fileAndPath6 $content6

###################################################################################

$content7="using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2._dd_;
using lssWebApi2._dd_;

namespace lssWebApi2.Controllers
{
    [Route(""api/[controller]"")]
    public class _xx_Controller : Controller
    {

[HttpPost]
        [Route(""View"")]
        [ProducesResponseType(typeof(_xx_View), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add_xx_([FromBody]_xx_View view)
        {
            _xx_Module invMod = new _xx_Module();

            NextNumber nn_xx_ = await invMod._xx_.Query().GetNextNumber();

            view._xx_Number = nn_xx_.NextNumberValue;

            _xx_ _yy_ = await invMod._xx_.Query().MapToEntity(view);

            invMod._xx_.Add_xx_(_yy_).Apply();

            _xx_View newView = await invMod._xx_.Query().GetViewByNumber(view._xx_Number);


            return Ok(newView);

        }

        [HttpDelete]
        [Route(""View"")]
        [ProducesResponseType(typeof(_xx_View), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete_xx_([FromBody]_xx_View view)
        {
            _xx_Module invMod = new _xx_Module();
            _xx_ _yy_ = await invMod._xx_.Query().MapToEntity(view);
            invMod._xx_.Delete_xx_(_yy_).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route(""View"")]
        [ProducesResponseType(typeof(_xx_View), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update_xx_([FromBody]_xx_View view)
        {
            _xx_Module invMod = new _xx_Module();

            _xx_ _yy_ = await invMod._xx_.Query().MapToEntity(view);


            invMod._xx_.Update_xx_(_yy_).Apply();

            _xx_View retView = await invMod._xx_.Query().GetViewById(_yy_._xx_Id);


            return Ok(retView);

        }

        [HttpGet]
        [Route(""View/{_xx_Id}"")]
        [ProducesResponseType(typeof(_xx_View), StatusCodes.Status200OK)]

        public async Task<IActionResult> Get_xx_View(long _yy_Id)
        {
            _xx_Module invMod = new _xx_Module();

            _xx_View view = await invMod._xx_.Query().GetViewById(_yy_Id);
            return Ok(view);
        }
        }
	}
        ";

$content7=$content7 -replace "_xx_", $class;
$content7=$content7 -replace "_yy_", $concrete;
$content7=$content7 -replace "_dd_", $domain;

$content7

$fileName7="_xx_Controller.cs"
$fileName7=$fileName7 -replace "_xx_", $class

$fileName7

$fileAndPath7=($path + "\" + $fileName7)

OutputFile $path $fileName7 $fileAndPath7 $content7

###################################################################################

$content8="using lssWebApi2.AbstractFactory;
using lssWebApi2._dd_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2._dd_
{
    public class _xx_Module : AbstractModule
    {
        public Fluent_xx_ _xx_ = new Fluent_xx_();
    }
}
";

$content8=$content8 -replace "_xx_", $class
$content8=$content8 -replace "_yy_", $concrete
$content8=$content8 -replace "_dd_", $domain

$fileName8="_xx_Module.cs"
$fileName8=$fileName8 -replace "_xx_", $class

$fileName8

$fileAndPath8=($path + "\" + $fileName8)

OutputFile $path $fileName8 $fileAndPath8 $content8

###################################################################################

$content9="using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2._dd_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2._dd_
{

    public class Unit_xx_
    {

        private readonly ITestOutputHelper output;

        public Unit_xx_(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            _xx_Module _xx_Mod = new _xx_Module();

           _xx_View view = new _xx_View()
            {
                    Description = '_xx_ Test',
                    _xx_Code=99

            };
            NextNumber nnNextNumber = await _xx_Mod._xx_.Query().GetNextNumber();

            view._xx_Number = nnNextNumber.NextNumberValue;

            _xx_ _yy_ = await _xx_Mod._xx_.Query().MapToEntity(view);

            _xx_Mod._zz_.Add_xx_(_yy_).Apply();

            _xx_ new_xx_ = await _xx_Mod._xx_.Query().GetEntityByNumber(view._xx_Number);

            Assert.NotNull(new_xx_);

            new_xx_.Description = '_xx_ Test Update';

            _xx_Mod._zz_.Update_xx_(new_xx_).Apply();

            _xx_View updateView = await _xx_Mod._xx_.Query().GetViewById(new_xx_._xx_Id);

            Assert.Same(updateView.Description, '_xx_ Test Update');
              _xx_Mod._zz_.Delete_xx_(new_xx_).Apply();
            _xx_ lookup_xx_= await _xx_Mod._xx_.Query().GetEntityById(view._xx_Id);

            Assert.Null(lookup_xx_);
        }
       
      

    }
}
";

$content9=$content9 -replace "_xx_", $class
$content9=$content9 -replace "_yy_", $concrete
$content9=$content9 -replace "_dd_", $domain
$content9=$content9 -replace "_zz_", $modulename

$fileName9="Unit_xx_.cs"
$fileName9=$fileName9 -replace "_xx_", $class

$fileName9

$fileAndPath9=($path + "\" + $fileName9)

OutputFile $path $fileName9 $fileAndPath9 $content9
