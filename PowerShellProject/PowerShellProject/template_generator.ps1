###Overview:  This code snippet demonstrate how to use a powershell script to build a series of c# files for creating a restful end point for a data entity ####class and a poco class.   The file is removed if it exists and a new file with content is created using new-item and test-path and remove-item.  A file ###is called a leaf whereas a directory or folder is called a container.  The classes uses the unit of work pattern with a data repository and the fluent api ###interfaces for the fluent classes.

###Set-ExecutionPolicy Unrestricted -Scope Process

function OutputFile($path,$fileName,$fileAndPath,$content)
{
	$result=Test-Path $fileAndPath -PathType leaf
	if($result -eq $true)
	{
		Remove-Item �Path $fileAndPath
	}
	New-Item -Path $path -Name $fileName -ItemType "file" -Value $content
}
$path="c:\users\owner\tmp_script"
$result=Test-Path $path -PathType Container

if ($result -eq $false)
{
New-Item -ItemType directory -Path $path
}

$class="SalesOrderDetail"; ###_xx_
$concrete="salesOrderDetail"; ###_yy_
$parentclass="SalesOrder"; ###_zz_
$parentconcrete="salesOrder"; ###_zz2_
#################################################################################

$content0="
public interface I_xx_Repository
    {
        Task<List<_xx_>> GetDetailsBy_zz_Id(long _zz2_Id);
        Task<_xx_> GetEntityById(long _zz2_Id);
	Task<_xx_> GetEntityByNumber(long _yy_Number);
	Task<List<_xx_>> GetDetailsBy_zz_Id(long _zz2_Id);
    }
";

$content0=$content0 -replace "_xx_", $class
$content0=$content0 -replace "_yy_", $concrete
$content0=$content0 -replace "_zz_", $parentclass
$content0=$content0 -replace "_zz2", $parentconcrete

$content0

$fileName0="I_xx_Repository.cs"
$fileName0=$fileName0 -replace "_xx_", $class

$fileName0

$fileAndPath0=($path + "\" + $fileName0)

OutputFile $path $fileName0 $fileAndPath0 $content0
#################################################################################

$content1="    public class _xx_Repository: Repository<_xx_>, I_xx_Repository
    {
        ListensoftwaredbContext _dbContext;
        public _xx_Repository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
   
  public async Task<_xx_>GetEntityById(long _yy_Id)
        {
            return await _dbContext.FindAsync<_xx_>();
        }

 public async Task<_xx_> GetEntityByNumber(long _yy_Number)
        {
            var query = await (from detail in _dbContext._xx_
                               where detail._xx_Number == _yy_Number select detail).FirstOrDefaultAsync<_xx_>();
            return query;
        }
        public async Task<List<_xx_>> GetDetailsBy_zz_Id(long _zz2_Id)
        {
            var query = 
                await (from detail in _dbContext._xx_
                where detail._zz_Id == _zz_Id
                select detail).ToListAsync<_xx_>();
            return query;
        }
 }
";

$content1=$content1 -replace "_xx_", $class
$content1=$content1 -replace "_yy_", $concrete
$content1=$content1 -replace "_zz_", $parentclass
$content1=$content1 -replace "_zz2", $parentconcrete

$content1

$fileName1="_xx_Repository.cs"
$fileName1=$fileName1 -replace "_xx_", $class

$fileName1

$fileAndPath1=($path + "\" + $fileName1)

OutputFile $path $fileName1 $fileAndPath1 $content1


#################################################################################
$content2=" public interface IFluent_xx_
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
";

$content2=$content2 -replace "_xx_", $class
$content2=$content2 -replace "_yy_", $concrete

$content2

$fileName2="IFluent_xx_.cs"
$fileName2=$fileName2 -replace "_xx_", $class

$fileName2
$fileAndPath2=($path + "\" + $fileName2)


OutputFile $path $fileName2 $fileAndPath2 $content2

#################################################################################

$content3=" public interface IFluent_xx_Query
    {
	Task<_xx_View> GetViewByNumber(long _yy_Number);
}";

$content3=$content3 -replace "_xx_", $class

$content3

$fileName3="IFluent_xx_Query.cs"
$fileName3=$fileName3 -replace "_xx_", $class

$fileName3
$fileAndPath3=($path + "\" + $fileName3)


OutputFile $path $fileName3 $fileAndPath3 $content3

#################################################################################

$content4="  public class Fluent_xx_ :IFluent_xx_
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public Fluent_xx_() { }
        public IFluent_xx_Query Query()
        {
            return new Fluent_xx_Query(unitOfWork) as IFluent_xx_Query;
        }
        public IFluent_xx_ Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluent_xx_;
        }
        public IFluent_xx_ Add_xx_s(List<_xx_> newObjects)
        {
            unitOfWork._yy_lRepository.AddObjects(newObjects);
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
            unitOfWork._xx_Repository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluent_xx_;
        }
        public IFluent_xx_ Update_xx_(_xx_ updateObject) {
            unitOfWork._xx_Repository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluent_xx_;

        }
        public IFluent_xx_ Delete_xx_(_xx_ deleteObject) {
            unitOfWork._xx_Repository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluent_xx_;
        }
   	public IFluent_xx_ Delete_xx_s(List<_xx_> deleteObjects)
        {
            unitOfWork._yy_Repository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluent_xx_;
        }
}"

$content4=$content4 -replace "_xx_",$class
$content4=$content4 -replace "_yy_",$concrete

$fileName4="Fluent_xx_.cs"
$fileName4=$fileName4 -replace "_xx_", $class

$fileName4

$fileAndPath4=($path + "\" + $fileName4)

OutputFile $path $fileName4 $fileAndPath4 $content4

#################################################################################

$content5="public class Fluent_xx_Query:IFluent_xx_Query
    {
        private UnitOfWork _unitOfWork = null;
        public Fluent_xx_Query() { }
        public Fluent_xx_Query(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<_xx_> MapToEntity(_xx_View inputObject)
        {
            Mapper mapper = new Mapper();
            _xx_ outObject = mapper.Map<_xx_>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<_xx_>> MapToEntity(List<_xx_View> inputObjects)
        {
            List<_xx_> list = new List<_xx_>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                _xx_ outObject = mapper.Map<_xx_>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<_xx_View> MapToView(_xx_ inputObject)
        {
            Mapper mapper = new Mapper();
            _xx_View outObject = mapper.Map<_xx_View>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<List<_xx_>> GetDetailsBy_zz_Id(long _zz2_Id)
        {
            return await _unitOfWork._yy_Repository.GetDetailsBy_xx_Id(_zz2_Id);
        }
        public async Task<List<_xx_View>> GetDetailViewsBy_zz_Id(long _zz2_Id)
        {
            List<_xx_View> listViews = new List<_xx_View>();
            List<_xx_> list = await _unitOfWork._yy_Repository.GetDetailsBy_zz_Id(_zz2_Id);
            foreach (var item in list)
            {
                listViews.Add(await MapToView(item));
            }

            return listViews;
        }
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork._yy_Repository.GetNextNumber('_xx_Number');
        }
 public async Task<_xx_View> GetViewById(long _yy_Id)
        {
            _xx_ detailItem = await _unitOfWork._yy_Repository.GetEntityById(_yy_Id);

            return await MapToView(detailItem);
        }
 public async Task<_xx_View> GetViewByNumber(long _yy_Number)
        {
            _xx_ detailItem = await _unitOfWork._yy_Repository.GetEntityByNumber(_yy_Number);

            return await MapToView(detailItem);
        }

}";


$content5=$content5 -replace "_xx_", $class;
$content5=$content5 -replace "_yy_", $concrete;
$content5=$content5 -replace "_zz_", $parentclass
$content5=$content5 -replace "_zz2", $parentconcrete

$content5

$fileName5="Fluent_xx_Query.cs"
$fileName5=$fileName5 -replace "_xx_", $class

$fileName5

$fileAndPath5=($path + "\" + $fileName5)

OutputFile $path $fileName5 $fileAndPath5 $content5
#################################################################################

$content6="public interface IFluent_xx_Query
{
        Task<_xx_> MapToEntity(_xx_View inputObject);
        Task<List<_xx_>> MapToEntity(List<_xx_View> inputObjects);
  	Task<List<_xx_>> GetDetailsBySalesOrderId(long _zz2_Id);
        Task<List<_xx_View>> GetDetailViewsBy_zz_Id(long _zz2_Id);
    
        Task<_xx_View> MapToView(_xx_ inputObject);
        Task<NextNumber> GetNextNumber();
	Task<_xx_View> GetViewById(long _yy_Id);
	Task<_xx_View> GetViewByNumber(long _yy_Number);
}
"

$content6=$content6 -replace "_xx_", $class
$content6=$content6 -replace "_yy_", $concrete
$content6=$content6 -replace "_zz_", $parentclass
$content6=$content6 -replace "_zz2", $parentconcrete


$content6

$fileName6="IFluent_xx_Query.cs"
$fileName6=$fileName6 -replace "_xx_", $class

$fileName6

$fileAndPath6=($path + "\" + $fileName6)

OutputFile $path $fileName6 $fileAndPath6 $content6


