// pages/index.tsx

import React from 'react';
import Navbar from '@/components/NavBar';
import FoodItem from '@/components/FoodItem';

const Page = () => {
  // Here you would fetch your food items data from an API or local source
  const foodItems = [
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },
    {
      title: 'Hamburger',
      description: 'A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.',
      price: '50.00',
      imageUrl: 'https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg', // Replace with your image path
    },

    // ... other food items
  ];

  return (
    <div className="container mx-auto">
      <div className="flex flex-wrap justify-center">
        {foodItems.map((item) => (
          <FoodItem key={item.title} {...item} />
        ))}
      </div>
    </div>
  );
};

export default Page;
