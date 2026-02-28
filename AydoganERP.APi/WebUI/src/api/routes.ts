type Result = {
  code: number;
  message: string;
  data: Array<any>;
};

/** 
 * Dinamik rotaları getir
 * Not: Backend'de dinamik rota endpoint'i yok, statik rotalar kullanılıyor
 */
export const getAsyncRoutes = (): Promise<Result> => {
  // Statik rotalar kullanıldığı için boş array döndür
  return Promise.resolve({
    code: 0,
    message: "success",
    data: []
  });
};
