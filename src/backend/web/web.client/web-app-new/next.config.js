/** @type {import('next').NextConfig} */
const nextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "img.buzzfeed.com",
      },
      {
        protocol: "http",
        hostname: "localhost",
      },
    ],
  },

  async redirects() {
    return [
      {
        source: "/",
        destination: "/foods",
        permanent: true,
      },
    ];
  },
};

module.exports = nextConfig;
