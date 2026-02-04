let appConfig: any = null

export async function loadConfig() {
  const res = await fetch("/config.json?" + new Date().getTime()) // cache kırmak için
  console.log(res)
  appConfig = await res.json()
}

export function getConfig() {
  return appConfig
}