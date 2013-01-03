using System;
using FOnline;
using FOnline.AngelScript;
using Microsoft.CSharp.RuntimeBinder;

namespace FOnline
{
    public static class Tests
    {
        static int counter = 0;
        static int total = 0;
        public static void Assert(bool cond, string msg)
        {
            total++;
            if(cond)
            {
                counter++;
                Global.Log ("{0} passed.", msg);
            }
            else
                Global.Log ("{0} failed.", msg);
        }
        public static void asAssert(bool cond, IntPtr msg)
        {
            var ss = new ScriptString(msg);
            Assert(cond, ss.ToString());
        }
        public static void InitRun()
        {
            Global.Log ("Running tests...");
            counter = total = 0;
            
            try 
            {
                AngelScriptTests.Run();
                BindFuncTests.Run();
                ArrayTests.Run();
            }
            finally
            {
                Global.Log("{0} succeeded out of {1}.", counter, total);
            }
        }
        public static void StartRun()
        {
            Global.Log("Running tests...");
            //counter = total = 0;
            
            try
            {
                MemoryTests.Run ();
            }
            finally
            {
                Global.Log("{0} succeeded out of {1}.", counter, total);
            }
        }
    }
    public static class BindFuncTests
    {
        static int Mul(int x, int y)
        {
            return x * y;
        }
        static int GetPointer(IntPtr self)
        {
            return (int)self;
        }
        public static void Run()
        {
            var module_ = AngelScript.ScriptEngine.GetModule("test", AngelScript.asEGMFlags.asGM_ALWAYS_CREATE);
            ScriptEngine.CallPragmas(new string[]
                                     {
                "bindfunc",
                "void assert(bool, string@+) -> mono FOnline.Tests::asAssert",
                "bindfunc",
                "int mul(int x, int y) -> mono FOnline.BindFuncTests::Mul",
                "bindfunc",
                "int string::GetPointer() -> mono FOnline.BindFuncTests::GetPointer"
            });
            module_.AddScriptSection("test", @"
void Run()
{
    assert(mul(2,3)==6, ""Static bindfunc"");
    string s(""hi"");
    assert(s.GetPointer() > 0, ""Nonstatic bindfunc"");
}
");
            module_.Build();
            var module = module_ as dynamic;
            module.Run();
            Global.Log ("eee");
        }
    }
    public static class AngelScriptTests
    {
        static void assert(bool cond, string msg)
        {
            Tests.Assert(cond, msg);
        }
        public static void Tests_GetGlobalVariables()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("GlobalPrimitives",
                                     @"bool GlobalBool = true;
                                    int8 GlobalInt8 = -123;
                                    int16 GlobalInt16 = -1234;
                                    int GlobalInt = -123456;
                                    int64 GlobalInt64 = -12345678987654321;
                                    uint8 GlobalUInt8 = 123;
                                    uint16 GlobalUInt16 = 1234;
                                    uint GlobalUInt = 123456;
                                    uint64 GlobalUInt64 = 12345678987654321;
                                    float GlobalFloat = 1.23f;
                                    double GlobalDouble = 1.23;
                                    string GlobalString = ""test"";
                                    string@ GlobalStringHandle = ""test2"";
                                    dictionary Dict;
                                    ");
            module_.Build();
            
            dynamic module = module_ as dynamic;
            assert (module.GlobalBool, "Reading bool variable");
            assert (module.GlobalInt8 == -123, "Reading int8 variable");
            assert (module.GlobalInt16 == -1234, "Reading int16 variable");
            assert (module.GlobalInt == -123456, "Reading int variable");
            assert (module.GlobalInt64 == -12345678987654321, "Reading int64 variable");
            assert (module.GlobalUInt8 == 123, "Reading uint8 variable");
            assert (module.GlobalUInt16 == 1234, "Reading uint16 variable");
            assert (module.GlobalUInt == 123456, "Reading uint variable");
            assert (module.GlobalUInt64 == 12345678987654321, "Reading uint64 variable");
            assert (module.GlobalFloat == 1.23f, "Reading float variable");
            assert (module.GlobalDouble == 1.23, "Reading double variable");
            assert (module.GlobalString.ToString() == "test", "Reading string variable");
            assert (module.GlobalStringHandle.ToString() == "test2", "Reading string handle");
            assert (module.Dict != null, "Reading builtin type variable");
        }
        public static void Tests_SetGlobalVariables()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("GlobalPrimitives",
                                     @"bool GlobalBool = true;
                                    int8 GlobalInt8 = -123;
                                    int16 GlobalInt16 = -1234;
                                    int GlobalInt = -123456;
                                    int64 GlobalInt64 = -12345678987654321;
                                    uint8 GlobalUInt8 = 123;
                                    uint16 GlobalUInt16 = 1234;
                                    uint GlobalUInt = 123456;
                                    uint64 GlobalUInt64 = 12345678987654321;
                                    float GlobalFloat = 1.23f;
                                    double GlobalDouble = 1.23;
                                    string GlobalString = ""test"";
                                    string@ GlobalStringHandle = ""test2"";
                                    ");
            module_.Build();
            
            dynamic module = module_ as dynamic;
            module.GlobalBool = false;
            assert (!module.GlobalBool, "Set/get bool variable");
            module.GlobalInt8 = -125;
            assert (module.GlobalInt8 == -125, "Set/get int8 variable");
            module.GlobalInt16 = -2345;
            assert (module.GlobalInt16 == -2345, "Set/get int16 variable");
            module.GlobalInt = -234567;
            assert (module.GlobalInt == -234567, "Set/get int variable");
            module.GlobalUInt8 = 125;
            assert (module.GlobalUInt8 == 125, "Set/get uint8 variable");
            module.GlobalUInt16 = 2345;
            assert (module.GlobalUInt16 == 2345, "Set/get uint16 variable");
            module.GlobalUInt = 234567;
            assert (module.GlobalUInt == 234567, "Set/get uint variable");
            module.GlobalFloat = 0.5f;
            assert (module.GlobalFloat == 0.5f, "Set/get float variable");
            module.GlobalDouble = 0.25;
            assert (module.GlobalDouble == 0.25, "Set/get double variable");
        }
        public static void Tests_GetFields()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("ObjectFields",
                                     @"
                                    class TestClass
                                    {
                                        int foo;
                                        float bar;
                                        string s;
                                        TestClass()
                                        {
                                            foo = 150;
                                            bar = 2.5f;
                                            s = ""foobar"";
                                        }
                                    }
                                    TestClass Obj;
                                    ");
            module_.Build();
            
            dynamic module = module_ as dynamic;
            assert (module.Obj.foo == 150, "Reading int field");
            assert (module.Obj.bar == 2.5f, "Reading float field");
            assert (module.Obj.s.ToString() == "foobar", "Reading string field");
        }
        public static void Tests_SetFields()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("ObjectFields",
                                     @"
                                    class TestClass
                                    {
                                        int foo;
                                        float bar;
                                        string s;
                                        TestClass()
                                        {
                                            foo = 150;
                                            bar = 2.5f;
                                            s = ""foobar"";
                                        }
                                    }
                                    TestClass Obj;
                                    ");
            module_.Build();
            
            dynamic module = module_ as dynamic;
            module.Obj.foo = 2500;
            assert (module.Obj.foo == 2500, "Set/get int field");
            module.Obj.bar = 15.3f;
            assert (module.Obj.bar == 15.3f, "Set/get float field");
        }
        public static void Tests_Properties()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("properties",
                                     @"
interface IFoo
{
    int get_Id();
    void set_Id(int v);
}
class Foo : IFoo
{
    int i;
    Foo(int i) { this.i = i; }
    int get_Id() { return i; }
    void set_Id(int v) { this.i = v; }
}
Foo Obj(4);
IFoo@ Get(int i) { return Foo(i); }
");
            module_.Build();
            dynamic module = module_ as dynamic;
            
            assert(module.Obj.Id == 4, "Getter");
            module.Obj.Id = 5;
            assert(module.Obj.Id == 5, "Setter");
            dynamic foo = module.Get(10);
            assert(foo.Id == 10, "Iface getter");
            foo.Id = 15;
            assert(foo.Id == 15, "Iface setter");
        }
        public static void Tests_Methods()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("Methods", @"
class TestClass
{
    int One()
    {
        return 1;
    }
    int SquareI(int x)
    {
        return x * x;
    }
    float SquareF(float x)
    {
        return x * x;
    }
}
TestClass Obj;

interface IFoo
{
    int Method();
    int get_Prop();
}
class CFoo : IFoo
{
    int i;
    CFoo(int i) { this.i = i; }
    int Method() { return i; }
    int get_Prop() { return i; }
}
IFoo@ Foo = CFoo(5);
IFoo@ GetFoo() { return CFoo(6); }
");
            module_.Build ();
            
            dynamic module = module_ as dynamic;
            dynamic obj = module.Obj;
            assert (obj.One() == 1, "Calling parameterless method");
            assert (obj.SquareI(2) == 4, "Calling int method");
            assert (obj.SquareF(1.2f) == 1.44f, "Calling float method");
            
            dynamic foo = module.Foo;
            assert (foo.Method() == 5, "Calling interface method");
            assert (foo.Prop == 5, "Calling property accessor");
        }
        public static void Tests_GlobalFunctions()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("GlobalFunctions", @"
int One()
{
    return 1;
}
bool SomeTest(int i)
{
    return i % 2 == 0;
}
float Square(float x)
{
    return x * x;
}
int RefFunc(string& s)
{
    return s.length();
}
int HandleFunc(string@ s)
{
    return s.length();
}
shared interface IFoo
{
    int get_Prop();
}
class CFoo : IFoo
{
    CFoo(int i) { this.i = i; }
    int i;
    int get_Prop() { return i; }
}
IFoo@ GetAsInterface(int i) { return CFoo(i); }
CFoo@ GetNull() { return null; }
");
            module_.Build();
            
            dynamic module = module_ as dynamic;
            assert (module.One() == 1, "Calling parameterless function");
            assert (module.SomeTest(2), "Calling func returning true");
            assert (!module.SomeTest(3), "Calling func returning false");
            assert (module.Square(1.2f) == 1.44f, "Calling float function");
            
            var s = new ScriptString("hello");
            assert (module.RefFunc(s) == 5, "Calling byref function");
            assert (module.HandleFunc(s) == 5, "Calling handle function");
            
            assert (module.GetAsInterface(2) != null, "Calling function returning script object");
            assert(module.GetNull() == null, "Caling null returning function");
        }
        public static void Tests_Parameters()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("Parameters", @"
int i(int i) { return i; }
int8 i8(int8 i) { return i;}
void refi(int& i) { i = i + 1; }
void reff(float& f) { f = 2 * f; }
");
            module_.Build();
            dynamic module = module_ as dynamic;

            assert (module.i((byte)1) == 1, "Int from byte");
            assert (module.i((short)1) == 1, "Int from short");

            assert (module.i8(1) == 1, "Sbyte from int");
            // bla bla, some meaningful corner cases, no need to cover all normal ones

            object i = 2; // the only way to have pass by ref behaviour for value types is to box them beforehand
            module.refi(i);
            assert((int)i == 3, "Int reference modified");

            object f = (float)2.5;
            module.reff(f);
            assert ((float)f == 5.0, "Float reference modified");
        }

        public static void Tests_Array()
        {
            var module_ = ScriptEngine.GetModule ("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection ("Arrays", @"
array<bool> BoolArray = { true, false };
array<int> IntArray = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
array<float> FloatArray = { 0.0f, 0.1f, 0.2f };
array<double> DoubleArray = { 0.0, 0.01, 0.05 };
array<string> StringArray = { ""hello"", ""world"" };
array<string@> StringHandleArray = { ""hello"", ""world"" };
class Foo { int f; Foo(int i) { f = i; } }
array<Foo> ScriptObjectArray = { Foo(1), Foo(2), Foo(3) };
array<Foo@> ScriptObjectHandleArray = { Foo(2), Foo(3), Foo(4) };
interface IFace { int get_F(); }
class CFace : IFace { int get_F() { return 1; } }
array<IFace@> IFaceHandleArray = { CFace() };
"
            );
            module_.Build ();
            
            var module = module_ as dynamic;
            
            assert (module.BoolArray.Length == 2, "Array' length");
            assert (module.BoolArray[0], "BoolArray[0]");
            assert (!module.BoolArray[1], "BoolArray[1]");
            
            assert (module.IntArray.Length == 10, "Array' length");
            assert (module.IntArray[0] == 0, "IntArray[0]");
            assert (module.IntArray[9] == 9, "IntArray[9]");
            
            assert (module.FloatArray[0] == 0.0f, "FloatArray[0]");
            assert (module.FloatArray[2] == 0.2f, "FloatArray[2]");
            
            assert (module.DoubleArray[0] == 0.0, "DoubleArray[0]");
            assert (module.DoubleArray[2] == 0.05, "DoubleArray[2]");
            
            assert (module.StringArray[0].ToString () == "hello", "StringArray[0]");
            assert (module.StringArray[1].ToString () == "world", "StringArray[1]");
            
            assert (module.StringHandleArray[0].ToString () == "hello", "StringHandleArray[0]");
            assert (module.StringHandleArray[1].ToString () == "world", "StringHandleArray[1]");
            
            assert (module.ScriptObjectArray[0].f == 1, "ScriptObjectArray[0]");
            assert (module.ScriptObjectArray[2].f == 3, "ScriptObjectArray[1]");
            module.ScriptObjectArray[2].f = 4;
            assert (module.ScriptObjectArray[2].f == 4, "ScriptObjectArray[1] again");
            module.ScriptObjectArray[2].f = 3;

            assert (module.ScriptObjectHandleArray[0].f == 2, "ScriptObjectHandleArray[0]");
            assert (module.ScriptObjectHandleArray[2].f == 4, "ScriptObjectHandleArray[2]");
            module.ScriptObjectHandleArray[2].f = 5;
            assert (module.ScriptObjectHandleArray[2].f == 5, "ScriptObjectHandleArray[2] again");
            module.ScriptObjectHandleArray[2].f = 4;

            assert (module.IFaceHandleArray[0].F == 1, "Interface handle array");

            bool iter = true;
            int i = 0;
            int c = 0;
            foreach(var e in module.IntArray) 
            {
                c++;
                if(e != i++) 
                    iter = false;
            }
            assert (iter, "Iterating over value array");
            assert (c == 10, "Iteration count");
            iter = true;
            i = 1;
            c = 0;
            foreach(var o in module.ScriptObjectArray) 
            {
                c++;
                if (o.f != i++)
                    iter = false;
            }
            assert(iter, "Iterating over object array");
            assert (c == 3, "Iteration count");
            iter = true;
            i = 2;
            c = 0;
            foreach (var o in module.ScriptObjectHandleArray)
            {
                c++;
                if (o.f != i++)
                    iter = false;
            }
            assert(iter, "Iterating over handle array");
            assert (c == 3, "Iteration count");
        }
        public static void Tests_IllegalCalls()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("test", @"
int foo()
{
    return 1;
}
class TestClass
{
    int field;
    int method()
    {
        return field;
    }
}
TestClass Obj();
");
            module_.Build();
            var module = module_ as dynamic;
            
            try
            {
                module.Foobar();
                Tests.Assert (false, "Calling nonexisting function");
            }
            catch(RuntimeBinderException)
            {
                Tests.Assert (true, "Calling nonexisting function");
            }
            try
            {
                var x = module.Ohmy;
                Tests.Assert (false, "Getting nonexisting global variable");
            }
            catch (RuntimeBinderException)
            {
                Tests.Assert (true, "Getting nonexisting global variable");
            }
            try
            {
                module.Woosh = 5;
                Tests.Assert (false, "Setting nonexisting global variable");
            }
            catch(RuntimeBinderException)
            {
                Tests.Assert(true, "Setting nonexisting global variable");
            }
            try
            {
                module.Obj.Boo();
                Tests.Assert (false, "Calling nonexisting method");
            }
            catch(RuntimeBinderException)
            {
                Tests.Assert(true, "Calling nonexisting method");
            }
            try
            {
                var f = module.Obj.Field;
                Tests.Assert (false, "Getting nonexisting field");
            }
            catch(RuntimeBinderException)
            {
                Tests.Assert (true, "Getting nonexisting field");
            }
            try
            {
                module.Obj.BrtmBrtm = 55;
                Tests.Assert (false, "Setting nonexisting field");
            }
            catch(RuntimeBinderException)
            {
                Tests.Assert (true, "Setting nonexisting field");
            }
        }

        /*class RCArray : ScriptArray<DummyRC>
        {
            static readonly IntPtr type;
            public RCHandleArray()
                : base(type) { }
            internal RCHandleArray(IntPtr ptr)
                : base(ptr, true) { }
            static RCHandleArray() { type = ScriptArray.GetType("array<DummyRC@>"); }
            public override DummyRC FromNative(IntPtr ptr) { return (DummyRC)ptr; }
        }
        class RCHandleArray : HandleArray<DummyRC>
        {
            static readonly IntPtr type;
            public RCHandleArray()
                : base(type) { }
            internal RCHandleArray(IntPtr ptr)
                : base(ptr, true) { }
            static RCHandleArray() { type = ScriptArray.GetType("array<DummyRC@>"); }
            public override DummyRC FromNative(IntPtr ptr) { return (DummyRC)GetObjectAddress(ptr); }
        }*/
        public static void Tests_RC()
        {
            var module_ = ScriptEngine.GetModule("test", asEGMFlags.asGM_ALWAYS_CREATE);
            module_.AddScriptSection("test", 
@"
DummyRC@ CreateDRC() { return CreateDummyRC(); }
class Foo { DummyRC rc; DummyRC& get_Dummy() { return rc; } }
Foo Obj();
void RefFunc(DummyRC& rc)
{
    int c = rc.RefCount;
}
void RefInFunc(DummyRC&in rc)
{
    int c = rc.RefCount;
}
void RefOutFunc(DummyRC&out rc)
{
    int c = rc.RefCount;
}
void RefHandleFunc(DummyRC@& rc)
{
    int c = rc.RefCount;
}
void LogPassByRef()
{
    Log(""LogPassByRef"");
    DummyRC rc;
    Log(""-"");
    RefFunc(rc);
    Log(""-"");
    Log(""LogPassByRef"");
}
void LogPassByInRef()
{
    Log(""LogPassByInRef"");
    DummyRC rc;
    Log(""-"");
    RefInFunc(rc);
    Log(""-"");
    Log(""LogPassByInRef"");
}
void LogPassByOutRef()
{
    Log(""LogPassByOutRef"");
    DummyRC rc;
    Log(""-"");
    RefOutFunc(rc);
    Log(""-"");
    Log(""LogPassByOutRef"");
}
void LogPassHandleByRef()
{
    Log(""LogPassHandleByRef"");
    DummyRC @rc = CreateDummyRC();
    Log(""-"");
    RefHandleFunc(rc);
    Log(""-"");
    Log(""LogPassHandleByRef"");
}
array<DummyRC> ObjArray = { DummyRC(), DummyRC() };
array<DummyRC@> HandleArray = { @DummyRC(), @DummyRC() };
array<ValueDummy> ValueArray = { ValueDummy(), ValueDummy() };

void LogArrayRC()
{
    Log(""ArrayRC:"");
    DummyRC objA = ObjArray[0];
    Log(""objA: "" + objA.RefCount);
    DummyRC@ objH = HandleArray[0];
    Log(""objH: "" + objH.RefCount);
    Log(""---"");
}
int ValueAddress()
{
    Log(""""+ValueArray[0].GetAddress());
    return ValueArray[0].GetAddress();
}
");
            module_.Build();
            dynamic module = module_ as dynamic;
            //module.LogPassByRef();
            //module.LogPassByInRef();
            //module.LogPassByOutRef();
            //module.LogPassHandleByRef();
            dynamic drc = module.CreateDRC();
            assert(drc.RefCount == 1, "RC of returned dynamic ScriptObject");
            dynamic drc2 = module.Obj.Dummy;
            assert (drc2.RefCount == 2, "RC of returned reference");
            module.RefFunc(drc2);
            assert(drc2.RefCount == 2, "Pass by ref: RC unchanged");
            module.RefInFunc(drc2);
            assert (drc2.RefCount == 2, "Pass by inref: RC unchanged"); // this misses ctor/assign behaviour testing
            module.RefOutFunc(drc2);
            assert (drc2.RefCount == 2, "Pass by outref: RC unchanged"); // this misses ctor/assign behaviour testing
            //module.RefHandleFunc(drc);
            //assert(drc.RefCount == 1, "Pass by handle ref: RC unchanged");

            module.LogArrayRC();
            dynamic objA = module.ObjArray[0];
            assert (objA.RefCount == 1, "Obj array RC"); // new object and opAssign
            assert (objA.ctor == 1, "Obj array fetch ctor");
            assert(objA.assign == 1, "Obj array fetch assign");
            dynamic objH = module.HandleArray[0];
            assert (objH.RefCount == 2, "Obj handle array RC"); // one handle by array, on by our object
            dynamic objV = module.ValueArray[0]; // the test for it is whether it just doesn't crash trying to call AddRef behaviour
            assert(objV.GetAddress() != module.ValueAddress(), "Value array accessor copies element");
        }
        public static void Run()
        {
            Tests_GetGlobalVariables();
            Tests_SetGlobalVariables();
            Tests_GetFields();
            Tests_SetFields();
            Tests_Properties();
            Tests_GlobalFunctions();
            Tests_Methods();
            Tests_Parameters();
            Tests_Array();
            Tests_IllegalCalls();
            Tests_RC();
        }
    }
    public static class MemoryTests
    {
        static void Collect()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            Global.CollectScriptGarbage();
        }
        
        static Critter GetCritter()
        {
            return Global.GetCritter(5000003);
        }
        public static void CritterFromNative()
        {
            var cr = GetCritter();
            int rc = cr.RefCount;
            var ncr = (Critter)cr.ThisPtr; // this does not create new Critter object, so rc stays same
            Tests.Assert (cr.RefCount == rc, "RC unchanged.");
            ncr = null;
            Collect ();
            Tests.Assert(cr.RefCount == rc, "RC unchanged.");
        }
        public static void HandleArray()
        {
            var cr = GetCritter();
            int rc = cr.RefCount;
            HandleArray_scope(cr, rc);
            Collect ();
            Tests.Assert (cr.RefCount == rc, "Array collected, rc--.");
        }
        static void HandleArray_scope(Critter cr, int rc)
        {
            var arr = new CritterArray { cr };
            Tests.Assert (cr.RefCount == rc + 1, "Critter reference in array, rc++.");
            var cr2 = arr[0];
            Tests.Assert (cr.RefCount == rc + 1, "Fetching reference to same native resource, rc the same.");
            arr[0] = null;
            Tests.Assert (cr.RefCount == rc, "Reference cleared, rc--.");
            arr[0] = cr;
        }
        public static void Run()
        {
            CritterFromNative();
            HandleArray();
        }
    }
    public static class ArrayTests
    {
        static void AddRange()
        {
            var arr = new IntArray(new[] { 0, 1, 2 });
            arr.AddRange(new[] { 3, 4 });
            Tests.Assert(arr.Length == 5, "AddRange array length.");
            Tests.Assert(arr[3] == 3, "AddRange first added element.");
            Tests.Assert(arr[4] == 4, "AddRange last element.");
        }
        static void Iterating()
        {
            var arr = new IntArray(new [] { 0, 1, 2, 3 });
            bool iter = true;
            int i = 0;
            int c = 0;
            foreach(var e in arr) 
            {
                c++;
                if(e != i++)
                    iter = false;
            }
            Tests.Assert(iter, "Iterating over value array");
            Tests.Assert(c == arr.Length, "Iteration count");
        }
        public static void Run()
        {
            AddRange();
        }
    }
}

