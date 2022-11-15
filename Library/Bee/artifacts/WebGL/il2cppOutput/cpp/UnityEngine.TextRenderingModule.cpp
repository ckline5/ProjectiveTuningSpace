#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


struct InvokerActionInvoker0
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj)
	{
		method->invoker_method(methodPtr, method, obj, NULL, NULL);
	}
};
template <typename T1>
struct InvokerActionInvoker1;
template <typename T1>
struct InvokerActionInvoker1<T1*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};

// System.Action`1<UnityEngine.Font>
struct Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC;
// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// UnityEngine.Font
struct Font_tC95270EA3198038970422D78B74A7F2E218A96B6;
// UnityEngine.Material
struct Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// System.String
struct String_t;
// UnityEngine.TextMesh
struct TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.Font/FontTextureRebuildCallback
struct FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1;

IL2CPP_EXTERN_C RuntimeClass* Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t806C4A82D63BA5BEE007D75772441609D967BADA 
{
};
struct Il2CppArrayBounds;

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
};

// UnityEngine.Color
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	// System.Single UnityEngine.Color::r
	float ___r_0;
	// System.Single UnityEngine.Color::g
	float ___g_1;
	// System.Single UnityEngine.Color::b
	float ___b_2;
	// System.Single UnityEngine.Color::a
	float ___a_3;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};

struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.Font
struct Font_tC95270EA3198038970422D78B74A7F2E218A96B6  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
	// UnityEngine.Font/FontTextureRebuildCallback UnityEngine.Font::m_FontTextureRebuildCallback
	FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* ___m_FontTextureRebuildCallback_5;
};

struct Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields
{
	// System.Action`1<UnityEngine.Font> UnityEngine.Font::textureRebuilt
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* ___textureRebuilt_4;
};

// UnityEngine.Material
struct Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// System.Action`1<UnityEngine.Font>
struct Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC  : public MulticastDelegate_t
{
};

// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};

// UnityEngine.TextMesh
struct TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.Font/FontTextureRebuildCallback
struct FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1  : public MulticastDelegate_t
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// System.Void System.Action`1<System.Object>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) ;

// System.Void UnityEngine.TextMesh::get_color_Injected(UnityEngine.Color&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___ret0, const RuntimeMethod* method) ;
// System.Void UnityEngine.TextMesh::set_color_Injected(UnityEngine.Color&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___value0, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Combine(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m8B9D24CED35033C7FC56501DFE650F5CB7FF012C (Delegate_t* ___a0, Delegate_t* ___b1, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Remove(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m40506877934EC1AD4ADAE57F5E97AF0BC0F96116 (Delegate_t* ___source0, Delegate_t* ___value1, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Font::Internal_CreateFont(UnityEngine.Font,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___self0, String_t* ___name1, const RuntimeMethod* method) ;
// System.Void System.Action`1<UnityEngine.Font>::Invoke(T)
inline void Action_1_Invoke_mF7CAC85021DFCE6516FAD20C0421A1AF389A3D3E_inline (Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* __this, Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___obj0, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*, Font_tC95270EA3198038970422D78B74A7F2E218A96B6*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___obj0, method);
}
// System.Void UnityEngine.Font/FontTextureRebuildCallback::Invoke()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_inline (FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Font::HasCharacter(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18 (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* __this, int32_t ___c0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.String UnityEngine.TextMesh::get_text()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, const RuntimeMethod* method) 
{
	typedef String_t* (*TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*);
	static TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::get_text()");
	String_t* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// System.Void UnityEngine.TextMesh::set_text(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	typedef void (*TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*, String_t*);
	static TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::set_text(System.String)");
	_il2cpp_icall_func(__this, ___value0);
}
// System.Void UnityEngine.TextMesh::set_anchor(UnityEngine.TextAnchor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	typedef void (*TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*, int32_t);
	static TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::set_anchor(UnityEngine.TextAnchor)");
	_il2cpp_icall_func(__this, ___value0);
}
// System.Single UnityEngine.TextMesh::get_characterSize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, const RuntimeMethod* method) 
{
	typedef float (*TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*);
	static TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::get_characterSize()");
	float icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// System.Void UnityEngine.TextMesh::set_characterSize(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, float ___value0, const RuntimeMethod* method) 
{
	typedef void (*TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*, float);
	static TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::set_characterSize(System.Single)");
	_il2cpp_icall_func(__this, ___value0);
}
// UnityEngine.Color UnityEngine.TextMesh::get_color()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color_tD001788D726C3A7F1379BEED0260B9591F440C1F TextMesh_get_color_m128E5D16AA72D5284C70957253DEAEE4FBEB023E (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, const RuntimeMethod* method) 
{
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769(__this, (&V_0), NULL);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_0 = V_0;
		return L_0;
	}
}
// System.Void UnityEngine.TextMesh::set_color(UnityEngine.Color)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_color_mF08F30C3CD797C16289225B567724B9F07DC641E (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___value0, const RuntimeMethod* method) 
{
	{
		TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4(__this, (&___value0), NULL);
		return;
	}
}
// System.Void UnityEngine.TextMesh::get_color_Injected(UnityEngine.Color&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___ret0, const RuntimeMethod* method) 
{
	typedef void (*TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*);
	static TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::get_color_Injected(UnityEngine.Color&)");
	_il2cpp_icall_func(__this, ___ret0);
}
// System.Void UnityEngine.TextMesh::set_color_Injected(UnityEngine.Color&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4 (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___value0, const RuntimeMethod* method) 
{
	typedef void (*TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4_ftn) (TextMesh_t7E1981C7B03E50D5CA5A3AD5B0D9BB0AB6EE91F8*, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*);
	static TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.TextMesh::set_color_Injected(UnityEngine.Color&)");
	_il2cpp_icall_func(__this, ___value0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void UnityEngine.Font::add_textureRebuilt(System.Action`1<UnityEngine.Font>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font_add_textureRebuilt_m0C7E9998192691918BC92548EE955380AD63FF0B (Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_0 = NULL;
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_1 = NULL;
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_2 = NULL;
	{
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_0 = ((Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields*)il2cpp_codegen_static_fields_for(Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var))->___textureRebuilt_4;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_1 = V_0;
		V_1 = L_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_2 = V_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m8B9D24CED35033C7FC56501DFE650F5CB7FF012C(L_2, L_3, NULL);
		V_2 = ((Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)Castclass((RuntimeObject*)L_4, Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC_il2cpp_TypeInfo_var));
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_5 = V_2;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_6 = V_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*>((&((Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields*)il2cpp_codegen_static_fields_for(Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var))->___textureRebuilt_4), L_5, L_6);
		V_0 = L_7;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_8 = V_0;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)L_8) == ((RuntimeObject*)(Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void UnityEngine.Font::remove_textureRebuilt(System.Action`1<UnityEngine.Font>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font_remove_textureRebuilt_mCCA3614ED92E2BE1EAC5FCE2D7DDFEDB0DCDE127 (Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_0 = NULL;
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_1 = NULL;
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* V_2 = NULL;
	{
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_0 = ((Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields*)il2cpp_codegen_static_fields_for(Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var))->___textureRebuilt_4;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_1 = V_0;
		V_1 = L_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_2 = V_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m40506877934EC1AD4ADAE57F5E97AF0BC0F96116(L_2, L_3, NULL);
		V_2 = ((Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)Castclass((RuntimeObject*)L_4, Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC_il2cpp_TypeInfo_var));
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_5 = V_2;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_6 = V_1;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*>((&((Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields*)il2cpp_codegen_static_fields_for(Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var))->___textureRebuilt_4), L_5, L_6);
		V_0 = L_7;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_8 = V_0;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)L_8) == ((RuntimeObject*)(Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// UnityEngine.Material UnityEngine.Font::get_material()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364 (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* __this, const RuntimeMethod* method) 
{
	typedef Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* (*Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364_ftn) (Font_tC95270EA3198038970422D78B74A7F2E218A96B6*);
	static Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Font::get_material()");
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// System.Void UnityEngine.Font::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font__ctor_m9106C7F312AE77F6721001A5A3143951201AC841 (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F(__this, (String_t*)NULL, NULL);
		return;
	}
}
// System.Void UnityEngine.Font::InvokeTextureRebuilt_Internal(UnityEngine.Font)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font_InvokeTextureRebuilt_Internal_m86017C5A7B49F602937D8C32FC978B876AFC37F9 (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___font0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* G_B2_0 = NULL;
	Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* G_B1_0 = NULL;
	FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* G_B5_0 = NULL;
	FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* G_B4_0 = NULL;
	{
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_0 = ((Font_tC95270EA3198038970422D78B74A7F2E218A96B6_StaticFields*)il2cpp_codegen_static_fields_for(Font_tC95270EA3198038970422D78B74A7F2E218A96B6_il2cpp_TypeInfo_var))->___textureRebuilt_4;
		Action_1_tD91E4D0ED3C2E385D3BDD4B3EA48B5F99D39F1DC* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_0013;
	}

IL_000c:
	{
		Font_tC95270EA3198038970422D78B74A7F2E218A96B6* L_2 = ___font0;
		Action_1_Invoke_mF7CAC85021DFCE6516FAD20C0421A1AF389A3D3E_inline(G_B2_0, L_2, NULL);
	}

IL_0013:
	{
		Font_tC95270EA3198038970422D78B74A7F2E218A96B6* L_3 = ___font0;
		FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* L_4 = L_3->___m_FontTextureRebuildCallback_5;
		FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* L_5 = L_4;
		G_B4_0 = L_5;
		if (L_5)
		{
			G_B5_0 = L_5;
			goto IL_001f;
		}
	}
	{
		goto IL_0025;
	}

IL_001f:
	{
		FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_inline(G_B5_0, NULL);
	}

IL_0025:
	{
		return;
	}
}
// System.Boolean UnityEngine.Font::HasCharacter(System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Font_HasCharacter_m71A84FE036055880E1543D79A38FEFA495AD200B (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* __this, Il2CppChar ___c0, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		Il2CppChar L_0 = ___c0;
		bool L_1;
		L_1 = Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18(__this, L_0, NULL);
		V_0 = L_1;
		goto IL_000b;
	}

IL_000b:
	{
		bool L_2 = V_0;
		return L_2;
	}
}
// System.Boolean UnityEngine.Font::HasCharacter(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18 (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* __this, int32_t ___c0, const RuntimeMethod* method) 
{
	typedef bool (*Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18_ftn) (Font_tC95270EA3198038970422D78B74A7F2E218A96B6*, int32_t);
	static Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Font::HasCharacter(System.Int32)");
	bool icallRetVal = _il2cpp_icall_func(__this, ___c0);
	return icallRetVal;
}
// System.Void UnityEngine.Font::Internal_CreateFont(UnityEngine.Font,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F (Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___self0, String_t* ___name1, const RuntimeMethod* method) 
{
	typedef void (*Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F_ftn) (Font_tC95270EA3198038970422D78B74A7F2E218A96B6*, String_t*);
	static Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Font::Internal_CreateFont(UnityEngine.Font,System.String)");
	_il2cpp_icall_func(___self0, ___name1);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_Multicast(FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* currentDelegate = reinterpret_cast<FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_Open(FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(method);
}
void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_OpenStaticInvoker(FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method)
{
	InvokerActionInvoker0::Invoke(__this->___method_ptr_0, method, NULL);
}
void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_ClosedStaticInvoker(FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method)
{
	InvokerActionInvoker1< RuntimeObject* >::Invoke(__this->___method_ptr_0, method, NULL, __this->___m_target_2);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1 (FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)();
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Native function invocation
	il2cppPInvokeFunc();

}
// System.Void UnityEngine.Font/FontTextureRebuildCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FontTextureRebuildCallback__ctor_m1AF27FC83F3136E493F47015F99CE7A4E6BCA0BC (FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___method1);
	__this->___method_3 = ___method1;
	__this->___m_target_2 = ___object0;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___object0);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___method1);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___method1))
	{
		bool isOpen = parameterCount == 0;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___method1))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_Open;
			else
				{
					__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
		__this->___method_code_6 = (intptr_t)__this->___m_target_2;
	}
	__this->___extra_arg_5 = (intptr_t)&FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_Multicast;
}
// System.Void UnityEngine.Font/FontTextureRebuildCallback::Invoke()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D (FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D_inline (FontTextureRebuildCallback_t76D5E172DF8AA57E67763D453AAC40F0961D09B1* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___obj0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
