import React from "react";

type Args = {
  status: "success" | "error" | "pending";
};

const ApiStatus = ({ status }: Args) => {
  switch (status) {
    case "error":
      return <div>Error communicating with the backend</div>;
    case "pending":
      return <div>loading...</div>;
    default:
      throw Error("unknown API state");
  }
};

export default ApiStatus;
