const config = {
  baseApiUrl: "https://localhost:4000",
};

const currencyFormatter = Intl.NumberFormat("en-US", {
  style: "currency",
  currency: "ZAR",
  maximumFractionDigits: 0,
});

export default config;
export { currencyFormatter };
