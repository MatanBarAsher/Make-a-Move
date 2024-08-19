import { useCallback, useEffect, useState } from "react";

export const useAsync = (callback, dependencies = []) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(undefined);
  const [value, setValue] = useState(undefined);

  const callbackMemoized = useCallback(() => {
    setLoading(true);
    setError(undefined);
    setValue(undefined);

    callback()
      .then((res) => setValue(res))
      .catch((e) => setError(e))
      .finally(() => setLoading(false));
  }, dependencies);

  useEffect(() => {
    callbackMemoized();
  }, [callbackMemoized]);

  return { loading, error, value };
};
