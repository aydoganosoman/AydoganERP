import { http } from "@/utils/http";
import type {
  GroupDto,
  CreateGroupCommand,
  UpdateGroupCommand,
  CategoryDto,
  CreateCategoryCommand,
  UpdateCategoryCommand,
  TagGroupDto,
  CreateTagGroupCommand,
  UpdateTagGroupCommand,
  TagDto,
  CreateTagCommand,
  UpdateTagCommand,
  FolderDto,
  CreateFolderCommand,
  UpdateFolderCommand,
  DistrictDto,
  CityDto,
  CountyDto
} from "./types";

// ============== Groups ==============

/** Grup listesi getir */
export const getGroups = (params?: {
  companyId?: string;
  usageArea?: number;
  isActive?: boolean;
}) => {
  console.log("getGroups API call starting...", params);
  return http
    .request<GroupDto[]>("get", "/Groups", { params })
    .then(res => {
      console.log("getGroups API response:", res);
      return res;
    })
    .catch(err => {
      console.error("getGroups API error:", err);
      throw err;
    });
};

/** Grup detayı getir */
export const getGroupById = (id: string) => {
  return http.request<GroupDto>("get", `/Groups/${id}`);
};

/** Grup oluştur */
export const createGroup = (data: CreateGroupCommand) => {
  return http.request<GroupDto>("post", "/Groups", { data });
};

/** Grup güncelle */
export const updateGroup = (id: string, data: UpdateGroupCommand) => {
  return http.request<GroupDto>("put", `/Groups/${id}`, { data });
};

// ============== Categories ==============

/** Kategori listesi getir */
export const getCategories = (params?: {
  companyId?: string;
  groupId?: string;
  processType?: number;
  isActive?: boolean;
}) => {
  return http.request<CategoryDto[]>("get", "/Categories", { params });
};

/** Kategori detayı getir */
export const getCategoryById = (id: string) => {
  return http.request<CategoryDto>("get", `/Categories/${id}`);
};

/** Kategori oluştur */
export const createCategory = (data: CreateCategoryCommand) => {
  return http.request<CategoryDto>("post", "/Categories", { data });
};

/** Kategori güncelle */
export const updateCategory = (id: string, data: UpdateCategoryCommand) => {
  return http.request<CategoryDto>("put", `/Categories/${id}`, { data });
};

// ============== TagGroups ==============

/** Etiket grubu listesi getir */
export const getTagGroups = (params?: {
  companyId?: string;
  isActive?: boolean;
}) => {
  return http.request<TagGroupDto[]>("get", "/TagGroups", { params });
};

/** Etiket grubu detayı getir */
export const getTagGroupById = (id: string) => {
  return http.request<TagGroupDto>("get", `/TagGroups/${id}`);
};

/** Etiket grubu oluştur */
export const createTagGroup = (data: CreateTagGroupCommand) => {
  return http.request<TagGroupDto>("post", "/TagGroups", { data });
};

/** Etiket grubu güncelle */
export const updateTagGroup = (id: string, data: UpdateTagGroupCommand) => {
  return http.request<TagGroupDto>("put", `/TagGroups/${id}`, { data });
};

// ============== Tags ==============

/** Etiket listesi getir */
export const getTags = (params?: {
  companyId?: string;
  tagGroupId?: string;
  isActive?: boolean;
}) => {
  return http.request<TagDto[]>("get", "/Tags", { params });
};

/** Etiket detayı getir */
export const getTagById = (id: string) => {
  return http.request<TagDto>("get", `/Tags/${id}`);
};

/** Etiket oluştur */
export const createTag = (data: CreateTagCommand) => {
  return http.request<TagDto>("post", "/Tags", { data });
};

/** Etiket güncelle */
export const updateTag = (id: string, data: UpdateTagCommand) => {
  return http.request<TagDto>("put", `/Tags/${id}`, { data });
};

// ============== Folders ==============

/** Klasör listesi getir */
export const getFolders = (params?: {
  companyId?: string;
  documentType?: number;
  isActive?: boolean;
}) => {
  return http.request<FolderDto[]>("get", "/Folders", { params });
};

/** Klasör detayı getir */
export const getFolderById = (id: string) => {
  return http.request<FolderDto>("get", `/Folders/${id}`);
};

/** Klasör oluştur */
export const createFolder = (data: CreateFolderCommand) => {
  return http.request<FolderDto>("post", "/Folders", { data });
};

/** Klasör güncelle */
export const updateFolder = (id: string, data: UpdateFolderCommand) => {
  return http.request<FolderDto>("put", `/Folders/${id}`, { data });
};

// ============== ProductUnits ==============

import type { ProductUnitDto } from "./types";

/** Birim listesi getir */
export const getProductUnits = () => {
  return http.request<ProductUnitDto[]>("get", "/ProductUnits");
};

// ============== Districts ==============

/** İlçe listesi getir */
export const getDistricts = (cityId: number) => {
  return http.request<DistrictDto[]>("get", `/Districts?cityId=${cityId}`);
};

// ============== Cities ==============

/** İl listesi getir */
export const getCities = (countryId: number) => {
  return http.request<CityDto[]>("get", `/Cities?countryId=${countryId}`);
};

// ============== Counties ==============

/** İlçe listesi getir */
export const getCounties = () => {
  return http.request<CountyDto[]>("get", "/Counties");
};
