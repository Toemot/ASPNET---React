import { QueryClient, useMutation, useQuery } from "@tanstack/react-query";
import { Bid } from "../types/bid";
import axios, { AxiosError, AxiosResponse } from "axios";
import Problem from "../types/problems";
import config from "../config";

const useFetchBids = (houseId: number) => {
  return useQuery<Bid[], AxiosError<Problem>>({
    queryKey: ["bids", houseId],
    queryFn: () =>
      axios
        .get(`${config.baseApiUrl}/house/${houseId}`)
        .then((resp) => resp.data),
  });
};

const useAddBids = () => {
  const queryClient = new QueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, Bid>({
    mutationFn: (b) =>
      axios.post(`${config.baseApiUrl}/house/${b.houseId}/bids`, b),
    onSuccess: (_, bid) =>
      queryClient.invalidateQueries({
        queryKey: ["bids", bid.houseId],
      }),
  });
};

export { useFetchBids, useAddBids };
