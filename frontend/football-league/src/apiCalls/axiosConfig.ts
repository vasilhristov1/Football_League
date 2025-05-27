import axios, { type AxiosRequestConfig } from 'axios'

// CONSTANTS
import { SESION_STORAGE_TOKEN_KEY, BASE_URL } from '../shared/constants'

const caller = axios.create({
  baseURL: BASE_URL,
})

caller.interceptors.request.use((config) => {
  const token = localStorage.getItem(SESION_STORAGE_TOKEN_KEY)

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
})

caller.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error.response?.status

    if (status === 401 || status === 403) {
      localStorage.removeItem(SESION_STORAGE_TOKEN_KEY)

      window.location.href = '/login'
    }

    return Promise.reject(error)
  }
)

export const call = <ResponseType>(params: AxiosRequestConfig) =>
  caller.request<ResponseType>(params)
