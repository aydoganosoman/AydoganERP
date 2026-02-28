import { http } from "@/utils/http";

/** Backend Token model */
export interface TokenModel {
  token: string;
  refreshToken: string;
}

/** Backend User Auth model - Login response */
export interface UserAuthModel {
  id: string;
  role: number;
  companyId: string | null;
  name: string;
  title: string;
  email: string;
  apiKey: string;
  status: number;
  token: TokenModel;
}

/** Frontend uyumlu UserResult - store'un beklediği format */
export type UserResult = {
  code: number;
  message: string;
  data: {
    avatar: string;
    username: string;
    nickname: string;
    roles: Array<string>;
    permissions: Array<string>;
    accessToken: string;
    refreshToken: string;
    expires: Date;
    companyId: string | null;
  };
};

export type RefreshTokenResult = {
  code: number;
  message: string;
  data: {
    accessToken: string;
    refreshToken: string;
    expires: Date;
  };
};

/** Login request */
export interface LoginRequest {
  email: string;
  password: string;
}

/** Giriş yap */
export const getLogin = (data: { username: string; password: string }) => {
  // Backend'e gönder
  return http
    .request<UserAuthModel>("post", "/Auth/Login", {
      data: {
        email: data.username,
        password: data.password
      }
    })
    .then(response => {
      // Backend yanıtını frontend formatına dönüştür
      const userAuth = response as UserAuthModel;
      const result: UserResult = {
        code: 0,
        message: "success",
        data: {
          avatar: "",
          username: userAuth.email,
          nickname: userAuth.name,
          roles: [getRoleName(userAuth.role)],
          permissions: ["*"],
          accessToken: userAuth.token.token,
          refreshToken: userAuth.token.refreshToken,
          expires: new Date(Date.now() + 24 * 60 * 60 * 1000), // 24 saat
          companyId: userAuth.companyId
        }
      };
      return result;
    });
};

/** Token doğrula / yenile */
export const refreshTokenApi = (data: { refreshToken: string; accessToken?: string }) => {
  return http
    .request<UserAuthModel>("post", "/Auth/VerifyToken", {
      data: {
        token: data.accessToken || "",
        refreshToken: data.refreshToken
      }
    })
    .then(response => {
      const userAuth = response as UserAuthModel;
      const result: RefreshTokenResult = {
        code: 0,
        message: "success",
        data: {
          accessToken: userAuth.token.token,
          refreshToken: userAuth.token.refreshToken,
          expires: new Date(Date.now() + 24 * 60 * 60 * 1000)
        }
      };
      return result;
    });
};

/** Firma kaydı */
export interface RegisterCompanyRequest {
  name: string;
  email: string;
}

export const registerCompany = (data: RegisterCompanyRequest) => {
  return http.request<void>("post", "/Auth/RegisterCompany", { data });
};

/** Role numarasını isme çevir */
function getRoleName(role: number): string {
  switch (role) {
    case 1:
      return "superadmin";
    case 2:
      return "companyadmin";
    case 3:
      return "customeruser";
    default:
      return "user";
  }
}
