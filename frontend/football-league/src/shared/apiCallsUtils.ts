import qs from 'qs'
import { isValidDate } from './utils'

export const createQueryString = (obj: unknown) =>
    qs.stringify(obj, {
      indices: false,
      addQueryPrefix: true,
      filter: (_, value: unknown) =>
        isValidDate(value) ? (value as Date).toISOString() : value || undefined,
    })