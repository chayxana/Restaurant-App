import * as React from 'react';
import { NavigateFunction } from 'react-router';
import { IFoodDto } from 'src/api/dtos/FoodDto';
import FoodItem from './FoodItem';

interface Props {
  foods: IFoodDto[];
  navigate: NavigateFunction;
}

const FoodsList: React.FC<Props> = props => {
  return (
    <div style={{ flex: 1 }}>
      {props.foods.map(item => {
        return <FoodItem {...props} key={item.id} item={item} />;
      })}
    </div>
  );
};

export default FoodsList;